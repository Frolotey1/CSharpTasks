
using System;

namespace Patterns;

public class VectorSvgRenderingStrategy : IRenderingStrategy
{
    public void DrawBackground(Rectangle rect, Color fill)
    {
        Console.WriteLine($"[SVG] <rect x='{rect.X}' y='{rect.Y}' width='{rect.Width}' height='{rect.Height}' fill='rgb({fill.R},{fill.G},{fill.B})' />");
    }

    public void DrawBorder(Rectangle rect, Color stroke, float thickness)
    {
        Console.WriteLine($"[SVG] <rect x='{rect.X}' y='{rect.Y}' width='{rect.Width}' height='{rect.Height}' stroke='rgb({stroke.R},{stroke.G},{stroke.B})' stroke-width='{thickness}' fill='none' />");
    }

    public void DrawText(string text, FontMetrics font, Point position, Color color)
    {
        Console.WriteLine($"[SVG] <text x='{position.X}' y='{position.Y}' font-family='{font.FontFamily}' font-size='{font.Size}' fill='rgb({color.R},{color.G},{color.B})'>{text}</text>");
    }

    public bool HitTest(Rectangle bounds, Point cursor)
    {
        return bounds.Contains(cursor);
    }

    public void DisposeResources()
    {
        Console.WriteLine("[SVG] Resources disposed");
    }

    public void Dispose()
    {
        DisposeResources();
        GC.SuppressFinalize(this);
    }
}

