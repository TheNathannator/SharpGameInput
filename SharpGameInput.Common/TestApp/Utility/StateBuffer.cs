using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpGameInput.TestApp.Utility
{
    public class StateBuffer
    {
        private struct AppendBuffer
        {
            public byte[] Data = [];
            public int Length = 0;

            public AppendBuffer()
            {
                Data = [];
                Length = 0;
            }

            public AppendBuffer(int capacity)
            {
                Data = [];

                Resize(capacity);
                Length = 0;
            }

            public bool Write(ReadOnlySpan<byte> data)
            {
                if (data.Length == Length && data.SequenceEqual(Data.AsSpan(0, Length)))
                {
                    return false;
                }

                Resize(data.Length);
                data.CopyTo(Data.AsSpan(0, data.Length));

                return true;
            }

            private void Resize(int length)
            {
                if (length > Data.Length)
                {
                    int newLength = Math.Max(Data.Length, 16);
                    while (newLength < length)
                    {
                        newLength *= 2;
                    }

                    Array.Resize(ref Data, newLength);
                }

                Length = length;
            }
        }

        private List<AppendBuffer> _buffers = new()
        {
            new(16),
        };

        private int _appendIndex = 0;

        public bool Write(Span<byte> data)
        {
            return Write((ReadOnlySpan<byte>)data);
        }

        public bool Write(ReadOnlySpan<byte> data)
        {
            var buffer = _buffers[0];
            bool result = buffer.Write(data);
            _buffers[0] = buffer;
            return result;
        }

        public bool Write<T>(Span<T> data)
            where T : unmanaged
        {
            return Write(MemoryMarshal.AsBytes(data));
        }

        public bool Write<T>(ReadOnlySpan<T> data)
            where T : unmanaged
        {
            return Write(MemoryMarshal.AsBytes(data));
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

        public bool Append(Span<byte> data)
        {
            return Append((ReadOnlySpan<byte>)data);
        }

        public bool Append(ReadOnlySpan<byte> data)
        {
            AppendBuffer buffer;
            if (_appendIndex >= _buffers.Count)
            {
                buffer = new();
                _appendIndex = _buffers.Count;
                _buffers.Add(buffer);
            }
            else
            {
                buffer = _buffers[_appendIndex];
            }

            bool result = buffer.Write(data);
            _buffers[_appendIndex++] = buffer;
            return result;
        }

        public bool Append<T>(Span<T> data)
            where T : unmanaged
        {
            return Append(MemoryMarshal.AsBytes(data));
        }

        public bool Append<T>(ReadOnlySpan<T> data)
            where T : unmanaged
        {
            return Append(MemoryMarshal.AsBytes(data));
        }

        public unsafe bool Append<T>(in T value)
            where T : unmanaged
        {
            fixed (void* ptr = &value)
            {
                var data = new ReadOnlySpan<byte>(ptr, sizeof(T));
                return Append(data);
            }
        }

        public void ResetAppend()
        {
            _appendIndex = 0;
        }
    }
}
