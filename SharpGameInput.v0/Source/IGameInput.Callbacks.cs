using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using SharpGameInput.Common;

namespace SharpGameInput.v0
{
    using unsafe ReadingCallback_NativePtr = delegate* unmanaged[Stdcall]<
        ulong, // callbackToken
        void*, // context
        IntPtr, // reading: IGameInputReading
        bool, // hasOverrunOccurred
        void // <return>
    >;

    using unsafe DeviceCallback_NativePtr = delegate* unmanaged[Stdcall]<
        ulong, // callbackToken
        void*, // context
        IntPtr, // device: IGameInputDevice
        ulong, // timestamp
        GameInputDeviceStatus, // currentStatus
        GameInputDeviceStatus, // previousStatus
        void // <return>
    >;

    using unsafe SystemButtonCallback_NativePtr = delegate* unmanaged[Stdcall]<
        ulong, // callbackToken
        void*, // context
        IntPtr, // device: IGameInputDevice
        ulong, // timestamp
        GameInputSystemButtons, // currentState
        GameInputSystemButtons, // previousState
        void // <return>
    >;

    using unsafe KeyboardLayoutCallback_NativePtr = delegate* unmanaged[Stdcall]<
        ulong, // callbackToken
        void*, // context
        IntPtr, // device: IGameInputDevice
        ulong, // timestamp
        uint, // currentLayout
        uint, // previousLayout
        void // <return>
    >;

    public unsafe partial class IGameInput
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void ReadingCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr reading, // IGameInputReading
            bool hasOverrunOccurred
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void DeviceCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            GameInputDeviceStatus currentStatus,
            GameInputDeviceStatus previousStatus
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void SystemButtonCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            GameInputSystemButtons currentState,
            GameInputSystemButtons previousState
        );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private unsafe delegate void KeyboardLayoutCallback_Native(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            uint currentLayout,
            uint previousLayout
        );

#if NET5_0_OR_GREATER
        private static readonly ReadingCallback_NativePtr _ReadingCallbackPtr = &ReadingCallback;
        private static readonly DeviceCallback_NativePtr _DeviceCallbackPtr = &DeviceCallback;
        private static readonly SystemButtonCallback_NativePtr _SystemButtonCallbackPtr = &SystemButtonCallback;
        private static readonly KeyboardLayoutCallback_NativePtr _KeyboardLayoutCallbackPtr = &KeyboardLayoutCallback;
#else
        private static readonly ReadingCallback_Native _ReadingCallback = ReadingCallback;
        private static readonly DeviceCallback_Native _DeviceCallback = DeviceCallback;
        private static readonly SystemButtonCallback_Native _SystemButtonCallback = SystemButtonCallback;
        private static readonly KeyboardLayoutCallback_Native _KeyboardLayoutCallback = KeyboardLayoutCallback;

        private static readonly ReadingCallback_NativePtr _ReadingCallbackPtr
            = (ReadingCallback_NativePtr)Marshal.GetFunctionPointerForDelegate(_ReadingCallback);
        private static readonly DeviceCallback_NativePtr _DeviceCallbackPtr
            = (DeviceCallback_NativePtr)Marshal.GetFunctionPointerForDelegate(_DeviceCallback);
        private static readonly SystemButtonCallback_NativePtr _SystemButtonCallbackPtr
            = (SystemButtonCallback_NativePtr)Marshal.GetFunctionPointerForDelegate(_SystemButtonCallback);
        private static readonly KeyboardLayoutCallback_NativePtr _KeyboardLayoutCallbackPtr
            = (KeyboardLayoutCallback_NativePtr)Marshal.GetFunctionPointerForDelegate(_KeyboardLayoutCallback);
#endif

        private static readonly ConcurrentDictionary<nint, IGameInput> _instances = new();
        private static nint _nextInstanceId = 0;

        private readonly nint _instanceId = _nextInstanceId++;
        private readonly ConcurrentDictionary<ulong, (object callback, object? context)> _callbacks = new();

        // Callback can be called while they are actively being registered, so we need to track
        // what's currently being registered so we can reference
        private readonly object callbackRegistrationLock = new();
        private (object? callback, object? context) _callbackBeingRegistered;

        protected override void DisposeManagedResources()
        {
            _instances.TryRemove(_instanceId, out _);
            base.DisposeManagedResources();
        }

        private void* MakeCallbackContext()
        {
            if (!_instances.ContainsKey(_instanceId) && !_instances.TryAdd(_instanceId, this))
            {
                throw new Exception("Failed to add this instance to the instance list!");
            }

            return (void*)_instanceId;
        }

        private static IGameInput ContextToIGameInput(void* context)
        {
            nint instanceId = (nint)context;
            if (!_instances.TryGetValue(instanceId, out var instance))
            {
                throw new Exception($"Invalid instance ID {instanceId}!");
            }

            return instance;
        }

        private (TCallback, object?) GetCallback<TCallback>(ulong callbackToken)
        {
            if (!_callbacks.TryGetValue(callbackToken, out var registration))
            {
                if (_callbackBeingRegistered.callback == null)
                {
                    throw new Exception($"Invalid callback token {callbackToken}!");
                }

                registration = _callbackBeingRegistered!;
            }

            return ((TCallback)registration.callback, registration.context);
        }

        public bool RegisterReadingCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            float analogThreshold,
            object? context,
            GameInputReadingCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            lock (callbackRegistrationLock)
            {
                _callbackBeingRegistered = (callbackFunc, context);
                result = RegisterReadingCallback(
                    device,
                    inputKind,
                    analogThreshold,
                    MakeCallbackContext(),
                    _ReadingCallbackPtr,
                    out ulong token
                );

                return FinishRegisteringCallback(result, token, callbackFunc, context, out callbackToken);
            }
        }

        public bool RegisterDeviceCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            GameInputDeviceStatus statusFilter,
            GameInputEnumerationKind enumerationKind,
            object? context,
            GameInputDeviceCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            lock (callbackRegistrationLock)
            {
                _callbackBeingRegistered = (callbackFunc, context);
                result = RegisterDeviceCallback(
                    device,
                    inputKind,
                    statusFilter,
                    enumerationKind,
                    MakeCallbackContext(),
                    _DeviceCallbackPtr,
                    out ulong token
                );

                return FinishRegisteringCallback(result, token, callbackFunc, context, out callbackToken);
            }
        }

        public bool RegisterSystemButtonCallback(
            IGameInputDevice? device,
            GameInputSystemButtons buttonFilter,
            object? context,
            GameInputSystemButtonCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            lock (callbackRegistrationLock)
            {
                _callbackBeingRegistered = (callbackFunc, context);
                result = RegisterSystemButtonCallback(
                    device,
                    buttonFilter,
                    MakeCallbackContext(),
                    _SystemButtonCallbackPtr,
                    out ulong token
                );

                return FinishRegisteringCallback(result, token, callbackFunc, context, out callbackToken);
            }
        }

        public bool RegisterKeyboardLayoutCallback(
            IGameInputDevice? device,
            object? context,
            GameInputKeyboardLayoutCallback callbackFunc,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken,
            out int result
        )
        {
            ThrowHelper.CheckNull(callbackFunc);

            lock (callbackRegistrationLock)
            {
                _callbackBeingRegistered = (callbackFunc, context);
                result = RegisterKeyboardLayoutCallback(
                    device,
                    MakeCallbackContext(),
                    _KeyboardLayoutCallbackPtr,
                    out ulong token
                );

                return FinishRegisteringCallback(result, token, callbackFunc, context, out callbackToken);
            }
        }

        public bool UnregisterCallback(ulong callbackToken, ulong timeoutInMicroseconds)
        {
            if (!_UnregisterCallback(callbackToken, timeoutInMicroseconds))
                return false;

            if (!_callbacks.TryRemove(callbackToken, out _))
            {
                // This should never happen; throw to notify of the problem
                throw new Exception("Failed to remove internal callback function reference, this should never happen!");
            }

            return true;
        }

        private bool FinishRegisteringCallback(
            int result,
            ulong token,
            object callbackFunc,
            object? callbackContext,
            [NotNullWhen(true)] out GameInputCallbackToken? callbackToken
        )
        {
            _callbackBeingRegistered = (null, null);

            if (result < 0)
            {
                callbackToken = null;
                return false;
            }

            if (!_callbacks.TryAdd(token, (callbackFunc, callbackContext)))
            {
                // This should never happen; make a best attempt to prevent the
                // callback from being leaked/called and then throw to notify of the problem
                StopCallback(token);
                _UnregisterCallback(token, 500);
                throw new Exception("A duplicate callback token has been encountered! This should never happen; if it does you're in some big trouble.");
            }

            callbackToken = new(this, token);
            return true;
        }

#if NET5_0_OR_GREATER
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
#endif
#if UNITY_STANDALONE
        [AOT.MonoPInvokeCallback(typeof(ReadingCallback_Native))]
#endif
        private static void ReadingCallback(
            ulong callbackToken,
            void* context,
            IntPtr reading, // IGameInputReading
            bool hasOverrunOccurred
        )
        {
            try
            {
                var gameInput = ContextToIGameInput(context);
                var (callbackFunc, callbackContext) = gameInput.GetCallback<GameInputReadingCallback>(callbackToken);

                callbackFunc(
                    new LightGameInputCallbackToken(gameInput, callbackToken),
                    callbackContext,
                    new LightIGameInputReading(reading, true),
                    hasOverrunOccurred
                );
            }
            catch (Exception ex)
            {
                OnUnhandledCallbackException(ex);
            }
        }

#if NET5_0_OR_GREATER
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
#endif
#if UNITY_STANDALONE
        [AOT.MonoPInvokeCallback(typeof(DeviceCallback_Native))]
#endif
        private static void DeviceCallback(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            GameInputDeviceStatus currentStatus,
            GameInputDeviceStatus previousStatus
        )
        {
            try
            {
                var gameInput = ContextToIGameInput(context);
                var (callbackFunc, callbackContext) = gameInput.GetCallback<GameInputDeviceCallback>(callbackToken);

                callbackFunc(
                    new LightGameInputCallbackToken(gameInput, callbackToken),
                    callbackContext,
                    new LightIGameInputDevice(device, false),
                    timestamp,
                    currentStatus,
                    previousStatus
                );
            }
            catch (Exception ex)
            {
                OnUnhandledCallbackException(ex);
            }
        }

#if NET5_0_OR_GREATER
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
#endif
#if UNITY_STANDALONE
        [AOT.MonoPInvokeCallback(typeof(SystemButtonCallback_Native))]
#endif
        private static void SystemButtonCallback(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            GameInputSystemButtons currentState,
            GameInputSystemButtons previousState
        )
        {
            try
            {
                var gameInput = ContextToIGameInput(context);
                var (callbackFunc, callbackContext) = gameInput.GetCallback<GameInputSystemButtonCallback>(callbackToken);

                callbackFunc(
                    new LightGameInputCallbackToken(gameInput, callbackToken),
                    callbackContext,
                    new LightIGameInputDevice(device, false),
                    timestamp,
                    currentState,
                    previousState
                );
            }
            catch (Exception ex)
            {
                OnUnhandledCallbackException(ex);
            }
        }

#if NET5_0_OR_GREATER
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
#endif
#if UNITY_STANDALONE
        [AOT.MonoPInvokeCallback(typeof(KeyboardLayoutCallback_Native))]
#endif
        private static void KeyboardLayoutCallback(
            ulong callbackToken,
            void* context,
            IntPtr device, // IGameInputDevice
            ulong timestamp,
            uint currentLayout,
            uint previousLayout
        )
        {
            try
            {
                var gameInput = ContextToIGameInput(context);
                var (callbackFunc, callbackContext) = gameInput.GetCallback<GameInputKeyboardLayoutCallback>(callbackToken);

                callbackFunc(
                    new LightGameInputCallbackToken(gameInput, callbackToken),
                    callbackContext,
                    new LightIGameInputDevice(device, false),
                    timestamp,
                    currentLayout,
                    previousLayout
                );
            }
            catch (Exception ex)
            {
                OnUnhandledCallbackException(ex);
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
