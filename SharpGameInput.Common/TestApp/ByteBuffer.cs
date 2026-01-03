using System;

namespace SharpGameInput.TestApp
{
    public class ByteBuffer
    {
        private byte[] _buffer = new byte[16];
        private int _length = 0;

        public bool Write(ReadOnlySpan<byte> data)
        {
            if (data.Length == _length && data.SequenceEqual(_buffer.AsSpan(0, _length)))
            {
                return false;
            }

            Resize(data.Length);
            data.CopyTo(_buffer.AsSpan(0, data.Length));
            _length = data.Length;

            return true;
        }

        public unsafe bool Write<T>(in T value)
            where T : unmanaged
        {
            fixed (void* ptr = &value)
            {
                var data = new ReadOnlySpan<byte>(ptr, sizeof(T));
                return Write(data);
            }
        }

        private void Resize(int targetLength)
        {
            if (targetLength <= _buffer.Length)
            {
                return;
            }

            int newLength = _buffer.Length;
            while (newLength < targetLength)
            {
                newLength *= 2;
            }

            Array.Resize(ref _buffer, newLength);
        }
    }
}
