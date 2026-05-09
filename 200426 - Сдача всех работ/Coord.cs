using System;

public readonly struct Coord {
    public int X { get; }
    public int Y { get; }

    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coord operator +(Coord a, Coord b)
    {
        return new Coord(a.X + b.X, a.Y + b.Y);
    }

    public static Coord operator -(Coord a, Coord b) {
        return new Coord(a.X - b.X, a.Y - b.Y);
    }

    public static Coord operator *(Coord a, int scalar) {
        return new Coord(a.X * scalar, a.Y * scalar);
    }

    public static bool operator ==(Coord a, Coord b) {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Coord a, Coord b) {
        return !(a == b);
    }

    public override bool Equals(object obj) {
        if (obj is Coord other) {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode() {
        return HashCode.Combine(X, Y);
    }
}
