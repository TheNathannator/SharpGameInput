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
                // This option should work for raw GIP reports as of GameInput v3.5
                gameInput.SetFocusPolicy(GameInputFocusPolicy.EnableBackgroundInput);

                // However, we still enable the window hack since we can't detect GameInput version
                if (!WindowHack.StartWindow())
                {
                    return;
                }

                TestMain.Run(gameInput);
            }
        }
    }
}