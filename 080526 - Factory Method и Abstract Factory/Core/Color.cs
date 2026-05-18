namespace Patterns;

public struct Color
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }

    public static Color Black => new Color { R = 0, G = 0, B = 0, A = 255 };
    public static Color White => new Color { R = 255, G = 255, B = 255, A = 255 };
    public static Color Red => new Color { R = 255, G = 0, B = 0, A = 255 };

    public Color(int r, int g, int b, int a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
