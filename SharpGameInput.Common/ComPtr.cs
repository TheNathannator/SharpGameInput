using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace SharpGameInput.Common
{
    public abstract class GameInputComPtr<TInterface> : CriticalFinalizerObject,
        IDisposable,
        IEquatable<GameInputComPtr<TInterface>>
        where TInterface : GameInputComPtr<TInterface>
    {
        protected internal IntPtr handle;
        private readonly bool ownsHandle;

        public bool IsInvalid => handle == IntPtr.Zero;

        internal GameInputComPtr(IntPtr handle, bool ownsHandle)
        {
            this.handle = handle;
            this.ownsHandle = ownsHandle;
        }

        ~GameInputComPtr() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
                DisposeManagedResources();
            DisposeUnmanagedResources();
        }

        protected virtual void DisposeManagedResources() { }

        protected virtual void DisposeUnmanagedResources()
        {
            if (handle != IntPtr.Zero && ownsHandle)
            {
                Marshal.Release(handle);
                handle = IntPtr.Zero;
            }
        }

        public IntPtr DangerousGetHandle() => handle;

        public TInterface Duplicate()
        {
            Marshal.AddRef(handle);
            return DuplicateImpl();
        }

        protected abstract TInterface DuplicateImpl();

        public static bool operator ==(GameInputComPtr<TInterface>? left, GameInputComPtr<TInterface>? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            // GameInput interfaces can be compared directly by pointer for equality
            return left.handle == right.handle;
        }

        public static bool operator !=(GameInputComPtr<TInterface>? left, GameInputComPtr<TInterface>? right)
            => !(left == right);

        public bool Equals([NotNullWhen(true)] GameInputComPtr<TInterface>? ptr)
            => ptr == this;

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is GameInputComPtr<TInterface> ptr && Equals(ptr);

        public override int GetHashCode()
            => handle.GetHashCode();
    }
}