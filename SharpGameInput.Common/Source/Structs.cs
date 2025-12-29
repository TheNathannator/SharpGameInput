using System.Runtime.InteropServices;

namespace SharpGameInput.Common
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct APP_LOCAL_DEVICE_ID
    {
        public const int Size = 32;

        internal fixed byte _value[Size];

        public byte this[int index]
        {
            get
            {
                ThrowHelper.CheckRange(index, Size);
                return _value[index];
            }
            set
            {
                ThrowHelper.CheckRange(index, Size);
                _value[index] = value;
            }
        }

        public readonly bool Equals(in APP_LOCAL_DEVICE_ID other)
        {
            fixed (byte* _l = _value)
            fixed (byte* _r = other._value)
            {
                long* l = (long*)_l;
                long* r = (long*)_r;
                return l[0] == r[0] &&
                    l[1] == r[1] &&
                    l[2] == r[2] &&
                    l[3] == r[3];
            }
        }

        public override int GetHashCode()
        {
            fixed (byte* _p = _value)
            {
                long* data = (long*)_p;
                return (data[0], data[1], data[2], data[3]).GetHashCode();
            }
        }

        public override string ToString()
        {
            const string characters = "0123456789ABCDEF";

            const int bufferSize = Size * 3;
            char* stringBuffer = stackalloc char[bufferSize];
            for (int i = 0; i < Size; i++)
            {
                byte v = _value[i];
                int stringIndex = i * 3;
                stringBuffer[stringIndex] = characters[(v & 0xF0) >> 4];
                stringBuffer[stringIndex + 1] = characters[v & 0x0F];
                stringBuffer[stringIndex + 2] = '-';
            }

            return new string(stringBuffer, 0, bufferSize - 1); // Exclude last '-'
        }
    }
}