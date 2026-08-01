    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct APP_LOCAL_DEVICE_ID :
        IEquatable<APP_LOCAL_DEVICE_ID>
    {
        public const int Size = 32;
        private const int LongSize = Size / sizeof(ulong);

        public fixed byte value[Size];

        public static bool operator ==(in APP_LOCAL_DEVICE_ID left, in APP_LOCAL_DEVICE_ID right)
        {
            fixed (byte* _l = left.value)
            fixed (byte* _r = right.value)
            {
                ulong* l = (ulong*)_l;
                ulong* r = (ulong*)_r;
                return l[0] == r[0] &&
                    l[1] == r[1] &&
                    l[2] == r[2] &&
                    l[3] == r[3];
            }
        }

        public static bool operator !=(in APP_LOCAL_DEVICE_ID left, in APP_LOCAL_DEVICE_ID right)
            => !(left == right);

        public readonly override bool Equals([NotNullWhen(true)] object? obj)
            => obj is APP_LOCAL_DEVICE_ID other && Equals(other);

        public readonly bool Equals(in APP_LOCAL_DEVICE_ID other)
            => this == other;

        readonly bool IEquatable<APP_LOCAL_DEVICE_ID>.Equals(APP_LOCAL_DEVICE_ID other)
            => this == other;

        public override int GetHashCode()
        {
            fixed (byte* _p = value)
            {
                ulong* data = (ulong*)_p;
                return (data[0], data[1], data[2], data[3]).GetHashCode();
            }
        }

        public override string ToString()
        {
            const string characters = "0123456789ABCDEF";

            const int bufferSize = Size * 2;
            char* stringBuffer = stackalloc char[bufferSize];
            for (int i = 0; i < Size; i++)
            {
                byte v = value[i];
                int stringIndex = i * 2;
                stringBuffer[stringIndex] = characters[(v & 0xF0) >> 4];
                stringBuffer[stringIndex + 1] = characters[v & 0x0F];
            }

            return new string(stringBuffer, 0, bufferSize);
        }
    }
