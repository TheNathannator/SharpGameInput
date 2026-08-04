using System.Text;

namespace SharpGameInput.Common
{
    internal static class StringHelper
    {
        public static unsafe string? FromUtf8(char_t* str)
        {
            if (str == null)
            {
                return null;
            }

            int length = StringLength(str);
            return Encoding.UTF8.GetString(str, length);
        }

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