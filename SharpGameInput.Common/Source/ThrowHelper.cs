using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SharpGameInput.Common
{
    internal static class ThrowHelper
    {
        public static void CheckNull([NotNull] object? obj, [CallerArgumentExpression(nameof(obj))] string name = "")
        {
#if NET7_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(obj, name);
#else
            if (obj is null)
                throw new ArgumentNullException(name);
#endif
        }

        public static unsafe void CheckNull([NotNull] void* ptr, [CallerArgumentExpression(nameof(ptr))] string name = "")
        {
#if NET7_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(ptr, name);
#else
            if (ptr == null)
                throw new ArgumentNullException(name);
#endif
        }

        public static void CheckDisposed([DoesNotReturnIf(true)] bool disposed, Type type)
        {
#if NET7_0_OR_GREATER
            ObjectDisposedException.ThrowIf(disposed, type);
#else
            if (disposed)
                throw new ObjectDisposedException(type.Name);
#endif
        }

        public static void CheckRange(int index, int size, [CallerArgumentExpression(nameof(index))] string name = "")
        {
#if NET7_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(index, name);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, size, name);
#else
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException(name);
#endif
        }
    }
}