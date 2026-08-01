    /// <summary>
    /// A wrapper for <c><see langword="bool"/></c>s that enables them to be blitted when marshalling.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(byte))]
    public struct ByteBool :
        IEquatable<ByteBool>,
        IEquatable<bool>,
        IComparable<ByteBool>,
        IComparable<bool>
    {
        private byte _value;

        public bool Value
        {
            get => _value != 0;
            set => _value = value ? (byte)1 : (byte)0;
        }

        public ByteBool(bool value)
        {
            _value = value ? (byte)1 : (byte)0;
        }

        public static implicit operator bool(ByteBool value)
            => value.Value;

        public static implicit operator ByteBool(bool value)
            => new(value);

        public static bool operator ==(ByteBool left, ByteBool right)
            => left.Value == right.Value;

        public static bool operator !=(ByteBool left, ByteBool right)
            => !(left == right);

        public readonly override bool Equals([NotNullWhen(true)] object? obj)
            => obj is ByteBool other && Equals(other);

        public readonly bool Equals(ByteBool other)
            => this == other;

        public readonly bool Equals(bool other)
            => this == other;

        public int CompareTo(ByteBool other)
            => Value.CompareTo(other.Value);

        public int CompareTo(bool other)
            => Value.CompareTo(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value.ToString();
    }
