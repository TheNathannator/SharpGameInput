using System;
using System.Linq;
using SharpGameInput.TestApp.Display;
using SharpGameInput.TestApp.Tests;

namespace SharpGameInput.TestApp
{
    public static class TestMain
    {
        public static void Run(IGameInput gameInput)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

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