using System;
using System.Linq;

namespace SharpGameInput.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (!WindowHack.StartWindow())
            {
                return;
            }

            // Initialize GameInput
            if (!GameInput.Create(out var gameInput, out int result))
            {
                ConsolePrinting.PrintPInvokeError("Failed to create IGameInput", result);
                ConsoleMenu.WaitForKey("Press any key to exit...");
                return;
            }

            using (gameInput)
            {
                ConsolePrinting.SetTimestampBase(gameInput.GetCurrentTimestamp());

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
                    ConsoleMenu.WriteMenuHeader("GameInput Tests", padHeader);
                    padHeader = true;

                    int choice = ConsoleMenu.PromptChoice("Select a test", tests.Select((i) => i.name));
                    if (choice < 0)
                        return;

                    tests[choice].func(gameInput);
                }
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Console.WriteLine("An unhandled exception has occured:");
            Console.WriteLine(args.ExceptionObject);
            ConsoleMenu.WaitForKey("Press any key to exit...");
        }
    }
}