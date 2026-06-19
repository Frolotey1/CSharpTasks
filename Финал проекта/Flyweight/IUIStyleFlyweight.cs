using System;
namespace Patterns;

public interface IUIStyleFlyweight {
    Guid StyleId { get; }
    FontMetrics Font { get; }
    ColorPalette Palette { get; }
    byte[]? IconBytes { get; }
}
