using System;
using System.Buffers;

namespace SharpGameInput.TestApp.Utility
{
    public ref struct StackOrPoolArray<T>
    {
        private T[]? _poolArray;
        private Span<T> _array;

        public Span<T> Array => _array;

        public StackOrPoolArray(int count, Span<T> stackArray)
        {
            if (count > stackArray.Length)
            {
                _poolArray = ArrayPool<T>.Shared.Rent(count);
                stackArray = _poolArray;
            }

            _array = stackArray.Slice(0, count);
        }

        public void Dispose()
        {
            if (_poolArray is {} array)
            {
                ArrayPool<T>.Shared.Return(array);
                _poolArray = null;
            }

            _array = default;
        }
    }
}