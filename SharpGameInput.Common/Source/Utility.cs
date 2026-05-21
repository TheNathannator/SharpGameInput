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

            checked
            {
                char_t* end = str;
                while (*end != 0)
                {
                    end++;
                }

                return (int)(end - str);
            }
        }

        public static unsafe int StringLength(wchar_t* str)
        {
            if (str == null)
            {
                return 0;
            }

            checked
            {
                wchar_t* end = str;
                while (*end != 0)
                {
                    end++;
                }

                return (int)(end - str);
            }
        }
    }
}