using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using SharpGameInput.Common;

namespace SharpGameInput.v0
{
    public unsafe partial class IGameInput
    {
        private CallbackRegistrar<IGameInput> _callbacks = new();

        protected override void DisposeManagedResources()
        {
            _callbacks.Dispose();
            base.DisposeManagedResources();
        }

        internal bool UnregisterCallback(ulong callbackToken, ulong timeoutInMicroseconds)
        {
            _callbacks.RemoveCallback(callbackToken);
            return _UnregisterCallback(callbackToken, timeoutInMicroseconds);
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
