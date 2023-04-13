namespace HoneycombProject
{
    public struct VecInt2
    {
        public int x;
        public int y;

#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static VecInt2 Up = new VecInt2(0, -1);
        public static VecInt2 Right = new VecInt2(1, 0);
        public static VecInt2 Down = new VecInt2(0, 1);
        public static VecInt2 Left = new VecInt2(-1, 0);
#pragma warning restore CA2211 // Non-constant fields should not be visible

        public static VecInt2 operator +(VecInt2 a, VecInt2 b)
        {
            return new VecInt2(a.x + b.x, a.y + b.y);
        }
        public static VecInt2 operator -(VecInt2 a, VecInt2 b)
        {
            return new VecInt2(a.x - b.x, a.y - b.y);
        }

        public static bool operator ==(VecInt2 a, VecInt2 b)
        {
            return !(a != b);
        }
        public static bool operator !=(VecInt2 a, VecInt2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public override string ToString()
        {
            return $"<{x}, {y}>";
        }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            return obj is VecInt2 @int &&
                   x == @int.x &&
                   y == @int.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public VecInt2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}