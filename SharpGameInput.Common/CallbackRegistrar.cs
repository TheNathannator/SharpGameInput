using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace SharpGameInput.Common
{
    internal unsafe class CallbackRegistrar<T> : IDisposable
    {
        public ref struct RegistrationScope
        {
            private readonly CallbackRegistrar<T> _registrar;
            private bool _lockTaken;

            public RegistrationScope(CallbackRegistrar<T> registrar, object callbackFunc, object? context)
            {
                _registrar = registrar;
                _lockTaken = false;

                try
                {
                    Monitor.Enter(registrar._registrationLock, ref _lockTaken);
                    
                    if (registrar._callbackBeingRegistered.callback != null)
                    {
                        throw new Exception("Another callback is in the process of being registered!");
                    }

                    registrar._callbackBeingRegistered = (callbackFunc, context);
                }
                catch
                {
                    if (_lockTaken)
                    {
                        Monitor.Exit(registrar._registrationLock);
                    }
                    throw;
                }
            }

            public bool Finish(int result, ulong token)
            {
                var (callbackFunc, context) = _registrar._callbackBeingRegistered;
                _registrar._callbackBeingRegistered = (null, null);

                if (result < 0 || token == InternalConstants.InvalidCallbackToken)
                {
                    return false;
                }

                if (callbackFunc == null)
                {
                    throw new Exception("There is no active callback registration to finish!");
                }

                if (!_registrar._callbacks.TryAdd(token, (callbackFunc!, context)))
                {
                    // This should never happen; make a best attempt to prevent the
                    // callback from being leaked/called and then throw to notify of the problem
                    throw new Exception("(!!UNREACHABLE!!) A duplicate callback token has been encountered! This is a bug in GameInput!");
                }

                return result >= 0;
            }

            public void Dispose()
            {
                if (_lockTaken)
                {
                    Monitor.Exit(_registrar._registrationLock);
                }
            }
        }

        private static readonly ConcurrentDictionary<nint, T> _instances = new();
        private static nint _nextInstanceId = 0;

        private readonly nint _instanceId = _nextInstanceId++;
        private readonly ConcurrentDictionary<ulong, (object callback, object? context)> _callbacks = new();

        // Callback can be called while they are actively being registered, so we need to track
        // what's currently being registered so we can reference
        private readonly object _registrationLock = new();
        private (object? callback, object? context) _callbackBeingRegistered;

        public void Dispose()
        {
            _instances.TryRemove(_instanceId, out _);
        }

        public void* MakeCallbackContext(T instance)
        {
            if (!_instances.ContainsKey(_instanceId) && !_instances.TryAdd(_instanceId, instance))
            {
                throw new Exception("Failed to add this instance to the instance list!");
            }

            return (void*)_instanceId;
        }

        public static T ContextToInstance(void* context)
        {
            nint instanceId = (nint)context;
            if (!_instances.TryGetValue(instanceId, out var instance))
            {
                throw new Exception($"Invalid instance ID {instanceId}!");
            }

            return instance;
        }

        public (object callbackFunc, object? context) GetCallback(ulong callbackToken)
        {
            if (!_callbacks.TryGetValue(callbackToken, out var registration))
            {
                if (_callbackBeingRegistered.callback == null)
                {
                    throw new Exception($"Invalid callback token {callbackToken}!");
                }

                registration = _callbackBeingRegistered!;
            }

            return (registration.callback, registration.context);
        }

        public RegistrationScope StartRegisteringCallback(object callbackFunc, object? context)
        {
            return new(this, callbackFunc, context);
        }

        public void RemoveCallback(ulong callbackToken)
        {
            if (!_callbacks.TryRemove(callbackToken, out _))
            {
                // This should never happen; throw to notify of the problem
                throw new Exception("Failed to remove internal callback function reference, this should never happen!");
            }
        }

        private static void OnUnhandledCallbackException(Exception exception)
        {
            // Stripped down from:
            // https://github.com/dotnet/runtime/blob/0d20f9ad3e0fd58a510062757b34f76a3c122b25/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs#L1900
            // Synchronization context is not handled since callbacks are run from unmanaged code,
            // so we go straight for the thread pool

            var dispatch = ExceptionDispatchInfo.Capture(exception);
            ThreadPool.QueueUserWorkItem(static state => ((ExceptionDispatchInfo)state!).Throw(), dispatch);
        }
    }
}
