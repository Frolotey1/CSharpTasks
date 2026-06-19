using System.Drawing;

namespace Patterns;

public readonly record struct LayoutContext(
    int Padding,
    int Spacing,
    Size AvailableSize,
    float DpiScale
);
