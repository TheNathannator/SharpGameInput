using System;
using System.Linq;

namespace SharpGameInput.v0.TestApp
{
    internal class Program
    {
        private static ulong _startupTimestamp;

        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Initialize GameInput
            if (!GameInput.Create(out var gameInput, out int result))
            {
                ConsoleUtility.WritePInvokeError("Failed to create IGameInput", result);
                ConsoleUtility.WaitForKey("Press any key to exit...");
                return;
            }

            using (gameInput)
            {
                _startupTimestamp = gameInput.GetCurrentTimestamp();

                // List of available tests
                (string name, Action<IGameInput> func)[] tests = 
                {
                    ("Read Raw Reports", RawReportTest.Run),
                    ("Callbacks", CallbacksTest.Run),
                };

                // Don't pad the header on startup, but do after startup
                bool padHeader = false;

                while (true)
                {
                    ConsoleUtility.WriteMenuHeader("GameInput Tests", padHeader);
                    padHeader = true;

                    int choice = ConsoleUtility.PromptChoice("Select a test", tests.Select((i) => i.name));
                    if (choice < 0)
                        return;

                    tests[choice].func(gameInput);
                }
            }

            static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
            {
                Console.WriteLine("An unhandled exception has occured:");
                Console.WriteLine(args.ExceptionObject);
                ConsoleUtility.WaitForKey("Press any key to exit...");
            }
        }

        public static TimeSpan TimestampToTimeSpan(ulong timestamp)
            => TimeSpan.FromSeconds(timestamp / 1000000.0);

        public static TimeSpan TimestampToTimeSpanFromStartup(ulong timestamp)
            => TimeSpan.FromSeconds((timestamp - _startupTimestamp) / 1000000.0);

        public static void PrintTimestamp(ulong timestamp)
        {
            var time = TimeSpan.FromSeconds(timestamp / 1000000.0);
            var timeSinceStartup = TimeSpan.FromSeconds((timestamp - _startupTimestamp) / 1000000.0);
            Console.Write($"{time} ({timeSinceStartup})");
        }
    }
}