namespace SharpGameInput.Common
{
    internal static class Utility
    {
        public static unsafe int StringLength(char_t* str)
        {
            if (str == null)
            {
                return 0;
            }

            int count = 0;
            while (*str != 0)
            {
                count++;
            }

            return count;
        }

        public static unsafe int StringLength(wchar_t* str)
        {
            if (str == null)
            {
                return 0;
            }

            int count = 0;
            while (*str != 0)
            {
                count++;
            }

            return count;
        }
    }
}