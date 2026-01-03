using System;
using System.Collections.Generic;
using System.Threading;

namespace SharpGameInput.TestApp.Display
{
    internal static partial class ConsoleMenu
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

        public static ConsoleKey WaitForKey(string message = "Press any key to continue...")
        {
            Console.WriteLine(message);

            // Give the message a bit of time to be present before allowing it to be dismissed
            Thread.Sleep(500);

            // Flush out any keys that are already in the read buffer to prevent accidental skipping
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }

            return Console.ReadKey(intercept: true).Key;
        }

        public static bool PromptYesNo(string message)
        {
            while (true)
            {
                Console.Write($"{message} (y/n) ");

                string? entry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(entry))
                {
                    string lower = entry.ToLowerInvariant();
                    if (lower == "y" || lower == "yes")
                    {
                        return true;
                    }
                    else if (lower == "n" || lower == "no")
                    {
                        return false;
                    }
                }

                Console.WriteLine("Invalid entry, please try again.");
            }
        }

        public static int PromptChoice(string title, string defaultOption, params string[] options)
            => PromptChoice(title, defaultOption, (IEnumerable<string>)options);

        public static int PromptChoice(string title, string defaultOption, IEnumerable<string> options)
        {
            // Title
            Console.WriteLine($"{title}:");

            // Options
            int count = 0;
            Console.WriteLine($"0. {defaultOption}");
            foreach (string option in options)
            {
                Console.WriteLine($"{++count}. {option}");
            }

            return PromptRange("Selection", 0, count) - 1;
        }

        public static int PromptRange(string title, int min, int max)
        {
            Console.Write($"{title}: ");
            while (true)
            {
                // Read selection
                string? entry = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entry) || !int.TryParse(entry, out int selection))
                {
                    Console.Write("Could not parse, please try again: ");
                    continue;
                }

                if (selection < min || selection > max)
                {
                    Console.Write("Selection out of range, please try again: ");
                    continue;
                }

                return selection;
            }
        }
    }
}