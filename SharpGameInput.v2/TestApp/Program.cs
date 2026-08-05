namespace SharpGameInput.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            if (TestMain.CreateGameInput(out var gameInput))
            {
                using (gameInput)
                {
                    // Version-specific setup is performed here
                    gameInput.SetFocusPolicy(
                        GameInputFocusPolicy.EnableBackgroundInput |
                        GameInputFocusPolicy.EnableBackgroundGuideButton |
                        GameInputFocusPolicy.EnableBackgroundShareButton
                    );
                    TestMain.Run(gameInput);
                }
            }
        }
    }
}