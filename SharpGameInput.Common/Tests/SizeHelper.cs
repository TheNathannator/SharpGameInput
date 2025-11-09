using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpGameInput.Common.Tests;

public static class SizeHelper
{
    public static unsafe void AssertSize<T>(int expected, bool checkMarshal = true)
        where T : unmanaged
    {
        Assert.Multiple(() =>
        {
            Assert.That(sizeof(T), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with sizeof(T)");
            Assert.That(Unsafe.SizeOf<T>(), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with Unsafe.SizeOf<T>()");
            if (checkMarshal)
            {
                Assert.That(Marshal.SizeOf<T>(), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with Marshal.SizeOf<T>()");
            }
        });
    }

    public static unsafe void AssertSize<T>(in T _, int expected, bool checkMarshal = true)
        where T : unmanaged
    {
        AssertSize<T>(expected, checkMarshal);
    }

    public static unsafe void AssertEnumSize<T>(int expected)
        where T : unmanaged, System.Enum
    {
        AssertSize<T>(expected, checkMarshal: false);
    }

    public static unsafe void AssertField<T, TField>(in T instance, in TField field, int size, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldName = "")
        where T : unmanaged
        where TField : unmanaged
    {
        AssertSize<TField>(size);
        Assert.That(OffsetOf(in instance, in field), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
        {
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
        }
    }

    public static unsafe void AssertField<T, TField>(in T instance, in TField* field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldName = "")
        where T : unmanaged
        where TField : unmanaged
    {
        Assert.That(OffsetOf(instance, field), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
        {
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
        }
    }

    public static unsafe void AssertField<T>(in T instance, in void* field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldName = "")
        where T : unmanaged
    {
        Assert.That(OffsetOf(instance, field), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
        {
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(offset), $"({typeof(T).Name}) {fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
        }
    }

    public static unsafe nint OffsetOf<T, TField>(in T instance, in TField field)
        where T : unmanaged
        where TField : unmanaged
    {
        fixed (void* instancePtr = &instance)
        fixed (void* fieldPtr = &field)
        {
            return (nint)fieldPtr - (nint)instancePtr;
        }
    }

    public static unsafe nint OffsetOf<T, TField>(in T instance, in TField* field)
        where T : unmanaged
        where TField : unmanaged
    {
        fixed (void* instancePtr = &instance)
        fixed (void* fieldPtr = &field)
        {
            return (nint)fieldPtr - (nint)instancePtr;
        }
    }

    public static unsafe nint OffsetOf<T>(in T instance, in void* field)
        where T : unmanaged
    {
        fixed (void* instancePtr = &instance)
        fixed (void* fieldPtr = &field)
        {
            return (nint)fieldPtr - (nint)instancePtr;
        }
    }
}