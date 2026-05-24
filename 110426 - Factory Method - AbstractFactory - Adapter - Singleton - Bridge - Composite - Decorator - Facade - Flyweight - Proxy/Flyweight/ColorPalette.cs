namespace Patterns;

public record ColorPalette {
    public Color Background { get; init; }
    public Color Foreground { get; init; }
    public Color Border { get; init; }
    public Color Hover { get; init; }

    public static ColorPalette Default => new() {
        Background = Color.White,
        Foreground = Color.Black,
        Border = Color.Black,
        Hover = Color.Red
    };
}
