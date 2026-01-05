using SharpGameInput.TestApp.Display;

namespace SharpGameInput.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            // Initialize GameInput
            if (!GameInput.Create(out var gameInput, out int result))
            {
                ConsolePrinting.PrintPInvokeError("Failed to create IGameInput", result);
                ConsoleMenu.WaitForKey("Press any key to exit...");
                return;
            }

            using (gameInput)
            {
                if (!WindowHack.StartWindow())
                {
                    return;
                }

                TestMain.Run(gameInput);
            }
        }
    }
}