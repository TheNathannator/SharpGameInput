using System;
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

    public static void AssertSize<T>(in T _, int expected, bool checkMarshal = true)
        where T : unmanaged
    {
        AssertSize<T>(expected, checkMarshal);
    }

    public static void AssertEnumSize<T>(int expected)
        where T : unmanaged, Enum
    {
        AssertSize<T>(expected, checkMarshal: false);
    }

    public static void AssertField<T, TField>(in T instance, in TField field, int size, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldExpr = "")
        where T : unmanaged
        where TField : unmanaged
    {
        AssertSize<TField>(size, checkMarshal: false);
        AssertField(instance, field, offset, checkMarshal, fieldExpr);
    }

    public static unsafe void AssertField<T, TField>(in T instance, in TField field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldExpr = "")
        where T : unmanaged
        where TField : unmanaged
    {
        fixed (void* fieldPtr = &field)
        {
            AssertFixed(instance, fieldPtr, offset, checkMarshal, fieldExpr);
        }
    }

    public static unsafe void AssertField<T, TField>(in T instance, in TField* field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldExpr = "")
        where T : unmanaged
        where TField : unmanaged
    {
        fixed (void* fieldPtr = &field)
        {
            AssertFixed(instance, fieldPtr, offset, checkMarshal, fieldExpr);
        }
    }

    public static unsafe void AssertField<T>(in T instance, in void* field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldExpr = "")
        where T : unmanaged
    {
        fixed (void* fieldPtr = &field)
        {
            AssertFixed(instance, fieldPtr, offset, checkMarshal, fieldExpr);
        }
    }

    public static unsafe void AssertFixed<T>(in T instance, void* field, nint offset, bool checkMarshal = true, [CallerArgumentExpression(nameof(field))] string fieldExpr = "")
        where T : unmanaged
    {
        string fieldName;
        int fieldStart;
        if ((fieldStart = fieldExpr.LastIndexOf('.')) >= 0 ||
            (fieldStart = fieldExpr.LastIndexOf(' ')) >= 0)
        {
            fieldName = fieldExpr[++fieldStart..];
        }
        else
        {
            fieldName = fieldExpr.Trim();
        }

        nint fieldOffset;
        fixed (void* instancePtr = &instance)
        {
            fieldOffset = (nint)((byte*)field - (byte*)instancePtr);
        }

        Assert.That(fieldOffset, Is.EqualTo(offset), $"{typeof(T).Name}.{fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
        {
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(offset), $"{typeof(T).Name}.{fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
        }
    }
}