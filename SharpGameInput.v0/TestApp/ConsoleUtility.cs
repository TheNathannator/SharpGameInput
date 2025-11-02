using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpGameInput.v0.TestApp
{
    internal static partial class ConsoleUtility
    {
        public static void WriteMenuHeader(string headerText, bool padHeader = true)
        {
            if (padHeader)
            {
                // Padding between previous section and new section
                Console.WriteLine();
            }

            // Write header
            string dashes = new string('-', headerText.Length);
            Console.WriteLine(dashes);
            Console.WriteLine(headerText);
            Console.WriteLine(dashes);

            // Padding between header and contents
            Console.WriteLine();
        }

        public static void WritePInvokeError(string message)
#if NET7_0_OR_GREATER
            => WritePInvokeError(message, Marshal.GetLastPInvokeError());
#else
            => WritePInvokeError(message, Marshal.GetLastWin32Error());
#endif

        public static void WritePInvokeError(string message, int error)
        {
#if NET7_0_OR_GREATER
            Console.WriteLine($"{message}: 0x{error:X8} ({Marshal.GetPInvokeErrorMessage(error)})");
#else
            Console.WriteLine($"{message}: 0x{error:X8}");
#endif
        }

        public static void WriteLine(ReadOnlySpan<byte> buffer)
        {
            const string characters = "0123456789ABCDEF";

            if (buffer.IsEmpty)
            {
                Console.WriteLine();
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
            stringBuffer = stringBuffer[..^1];

            Console.WriteLine(stringBuffer.ToString());
        }

        public static void WriteWrapped(ReadOnlySpan<byte> bytes, int indentAmount = 0, int wrapCount = 16)
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

        public static ConsoleKey WaitForKey(string message = "Press any key to continue...")
        {
            Console.WriteLine(message);
            return Console.ReadKey(intercept: true).Key;
        }

        public static bool PromptYesNo(string message)
        {
            while (true)
            {
                Console.Write($"{message} (y/n) ");

                string? entry = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entry))
                {
                    Console.WriteLine("Invalid entry, please try again.");
                }
                else
                {
                    var lower = entry.ToLowerInvariant();
                    if (lower == "y" || lower == "yes")
                    {
                        return true;
                    }
                    else if (lower == "n" || lower == "no")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry, please try again.");
                    }
                }
            }
        }

        public static int PromptChoice(string title, params string[] options)
            => PromptChoice(title, (IEnumerable<string>)options);

        public static int PromptChoice(string title, IEnumerable<string> options)
        {
            // Title
            Console.Write(title);
            Console.WriteLine(": ");

            // Options
            int count = 0;
            Console.WriteLine($"0. Exit");
            foreach (string option in options)
            {
                Console.WriteLine($"{++count}. {option}");
            }
            Console.Write("Selection: ");

            while (true)
            {
                int selection = PromptChoice_ReadSelection() - 1;
                if (selection < -1 || selection >= count)
                {
                    Console.Write("Invalid selection, please try again: ");
                    continue;
                }

                return selection;
            }
        }

        public static int PromptRange(string title, int min, int max)
        {
            // Title
            Console.Write(title);
            Console.WriteLine(": ");

            while (true)
            {
                int selection = PromptChoice_ReadSelection();
                if (selection < min || selection > max)
                {
                    Console.Write("Selection out of range, please try again: ");
                    continue;
                }

                return selection;
            }
        }

        private static int PromptChoice_ReadSelection()
        {
            while (true)
            {
                // Read selection
                string? entry = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entry) || !int.TryParse(entry, out int selection))
                {
                    Console.Write("Could not parse, please try again: ");
                    continue;
                }

                return selection;
            }
        }
    }
}