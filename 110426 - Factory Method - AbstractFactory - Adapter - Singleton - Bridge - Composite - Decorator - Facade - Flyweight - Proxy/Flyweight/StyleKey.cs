namespace Patterns;

public record struct StyleKey {
    public string FontFamily { get; init; }
    public int FontSize { get; init; }
    public string Theme { get; init; }

    public StyleKey(string fontFamily, int fontSize, string theme) {
        FontFamily = fontFamily;
        FontSize = fontSize;
        Theme = theme;
    }
}
