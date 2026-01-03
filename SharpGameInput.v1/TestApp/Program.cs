using SharpGameInput.TestApp.Display;

namespace SharpGameInput.TestApp
{
    internal class Program
    {
        private static void Main()
        {
            if (!WindowHack.StartWindow())
            {
                return;
            }

            TestMain.Run();
        }
    }
}