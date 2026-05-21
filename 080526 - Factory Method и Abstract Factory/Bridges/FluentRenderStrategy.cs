using System;

namespace Patterns;

public class FluentRenderingStrategy : IRenderingStrategy
{
    public void DrawBackground(Rectangle rect, Color fill)
    {
        Console.WriteLine($"[Fluent] Background: ({rect.X},{rect.Y},{rect.Width},{rect.Height})");
    }

    public void DrawBorder(Rectangle rect, Color stroke, float thickness)
    {
        Console.WriteLine($"[Fluent] Border: ({rect.X},{rect.Y},{rect.Width},{rect.Height})");
    }

    public void DrawText(string text, FontMetrics font, Point position, Color color)
    {
        Console.WriteLine($"[Fluent] Text: '{text}' at ({position.X},{position.Y})");
    }

    public bool HitTest(Rectangle bounds, Point cursor)
    {
        return bounds.Contains(cursor);
    }

    public void DisposeResources()
    {
        Console.WriteLine("[Fluent] Resources disposed");
    }

    public void Dispose()
    {
        DisposeResources();
        GC.SuppressFinalize(this);
    }
}
