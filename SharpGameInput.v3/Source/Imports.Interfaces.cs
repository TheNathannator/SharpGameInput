using System;
using SharpGameInput.Common;

namespace SharpGameInput.v3
{
    public partial class IGameInput
    {
        private readonly CallbackRegistrar<IGameInput> _callbacks = new();

        public event Action<Exception>? UnhandledCallbackException
        {
            add => _callbacks.UnhandledCallbackException += value;
            remove => _callbacks.UnhandledCallbackException -= value;
        }

        protected override void DisposeManagedResources()
        {
            _callbacks.Dispose();
            base.DisposeManagedResources();
        }

        internal bool UnregisterCallback(ulong callbackToken)
        {
            _callbacks.RemoveCallback(callbackToken);
            return _UnregisterCallback(callbackToken);
        }
    }
}
