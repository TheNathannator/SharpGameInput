using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpGameInput.TestApp.Display;
using SharpGameInput.TestApp.Tests;

namespace SharpGameInput.TestApp
{
    public static class TestMain
    {
        public static bool CreateGameInput([NotNullWhen(true)] out IGameInput? gameInput)
        {
            if (!GameInput.Create(out gameInput, out int result))
            {
                ConsolePrinting.PrintPInvokeError("Failed to create IGameInput", result);
                ConsoleMenu.WaitForKey("Press any key to exit...");
                return false;
            }

            return true;
        }

        public static void Run(IGameInput gameInput)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // A window is required to receive raw GIP reports up until GameInput v3.5
            if (!WindowHack.StartWindow())
            {
                return;
            }

            ConsolePrinting.SetTimestampBase(gameInput.GetCurrentTimestamp());

            // List of available tests
            (string name, Action<IGameInput> func)[] tests = 
            [
                ("Readings", ReadingsTest.Run),
                ("Callbacks", CallbacksTest.Run),
            ];

            // Don't pad the header on startup, but do after startup
            bool padHeader = false;

            while (true)
            {
                ConsoleMenu.WriteMenuHeader("GameInput Tests", padHeader);
                padHeader = true;

                int choice = ConsoleMenu.PromptChoice("Select a test", "Exit", tests.Select((i) => i.name));
                if (choice < 0)
                    return;

                tests[choice].func(gameInput);
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