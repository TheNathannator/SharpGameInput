    public abstract class GameInputComPtr : CriticalFinalizerObject,
        IDisposable,
        IEquatable<GameInputComPtr>
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

        protected virtual void DisposeManagedResources()
        {
        }

        protected virtual void DisposeUnmanagedResources()
        {
            if (handle != IntPtr.Zero && ownsHandle)
            {
                Marshal.Release(handle);
                handle = IntPtr.Zero;
            }
        }

        public IntPtr DangerousGetHandle() => handle;

        public static bool operator ==(GameInputComPtr? left, GameInputComPtr? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            // GameInput interfaces can be compared directly by pointer for equality
            return left.handle == right.handle;
        }

        public static bool operator !=(GameInputComPtr? left, GameInputComPtr? right)
            => !(left == right);

        public bool Equals([NotNullWhen(true)] GameInputComPtr? ptr)
            => this == ptr;

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is GameInputComPtr ptr && Equals(ptr);

        public override int GetHashCode()
            => handle.GetHashCode();
    }
