using System;
using System.Runtime.InteropServices;

namespace SharpGameInput.TestApp
{
    internal static partial class ConsolePrinting
    {
        public static void PrintPInvokeError(string message)
#if NET7_0_OR_GREATER
            => PrintPInvokeError(message, Marshal.GetLastPInvokeError());
#else
            => PrintPInvokeError(message, Marshal.GetLastWin32Error());
#endif

        public static void PrintPInvokeError(string message, int error)
        {
#if NET7_0_OR_GREATER
            Console.WriteLine($"{message}: 0x{error:X8} ({Marshal.GetPInvokeErrorMessage(error)})");
#else
            Console.WriteLine($"{message}: 0x{error:X8}");
#endif
        }

        public static void WriteBuffer(ReadOnlySpan<byte> buffer)
        {
            const string characters = "0123456789ABCDEF";

            if (buffer.IsEmpty)
            {
                return;
            }

            Span<char> stringBuffer = stackalloc char[buffer.Length * 3];
            for (int i = 0; i < buffer.Length; i++)
            {
                byte value = buffer[i];
                int stringIndex = i * 3;
                stringBuffer[stringIndex] = characters[(value & 0xF0) >> 4];
                stringBuffer[stringIndex + 1] = characters[value & 0x0F];
                stringBuffer[stringIndex + 2] = '-';
            }

            // Exclude last '-'
            stringBuffer = stringBuffer.Slice(0, stringBuffer.Length - 1);

            Console.Write(stringBuffer.ToString());
        }

        public static void PrintBufferWrapped(ReadOnlySpan<byte> bytes, int indentAmount = 0, int wrapCount = 16)
        {
            string indent = new string(' ', indentAmount);

            int index = 0;
            int count = 0;
            Console.Write(indent);
            for (; index < bytes.Length - 1; index++)
            {
                if (count < wrapCount - 1)
                {
                    // Continue on same line
                    Console.Write($"{bytes[index]:X2}-");
                    count++;
                }
                else
                {
                    // End line and create a new one
                    Console.WriteLine($"{bytes[index]:X2}");
                    Console.Write(indent);
                    count = 0;
                }
            }
            // Write last element without the hyphen at the start
            Console.WriteLine($"{bytes[index]:X2}");
        }
    }
}