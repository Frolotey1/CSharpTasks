using System;
namespace Patterns;

public record UIStyleFlyweight : IUIStyleFlyweight {
    public Guid StyleId { get; init; } = Guid.NewGuid();
    public FontMetrics Font { get; init; }
    public ColorPalette Palette { get; init; }
    public byte[]? IconBytes { get; init; }

    public UIStyleFlyweight(FontMetrics font, ColorPalette palette, byte[]? iconBytes = null) {
        Font = font;
        Palette = palette;
        IconBytes = iconBytes;
    }
}
