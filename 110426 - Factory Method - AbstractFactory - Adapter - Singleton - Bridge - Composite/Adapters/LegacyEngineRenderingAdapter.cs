using System;

namespace Patterns;

public class LegacyEngineRenderingAdapter : IRenderingStrategy
{
    private readonly LegacyGraphicsEngine _legacyEngine;
    private readonly IApplicationTelemetry _telemetry;

    public LegacyEngineRenderingAdapter(LegacyGraphicsEngine legacyEngine, IApplicationTelemetry telemetry)
    {
        _legacyEngine = legacyEngine;
        _telemetry = telemetry;
    }

    public void DrawBackground(Rectangle rect, Color fill)
    {
        var startTime = DateTime.UtcNow;
        
        _legacyEngine.RenderTextRaster("Arial", 10, fill.R, fill.G, fill.B, rect.X, rect.Y, "█");
        
        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Adapter", "DrawBackground", duration, $"rect={rect.X},{rect.Y},{rect.Width},{rect.Height}");
    }

    public void DrawBorder(Rectangle rect, Color stroke, float thickness)
    {
        var startTime = DateTime.UtcNow;
        
        _legacyEngine.DrawNativeButton(rect.X, rect.Y, rect.Width, rect.Height, $"Border");
        
        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Adapter", "DrawBorder", duration, $"thickness={thickness}");
    }

    public void DrawText(string text, FontMetrics font, Point position, Color color)
    {
        var startTime = DateTime.UtcNow;
        
        _legacyEngine.RenderTextRaster(
            font.FontFamily, 
            font.Size, 
            color.R, color.G, color.B, 
            position.X, position.Y, 
            text);
        
        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Adapter", "DrawText", duration, $"text={text}");
    }

    public bool HitTest(Rectangle bounds, Point cursor)
    {
        return bounds.Contains(cursor);
    }

    public void DisposeResources()
    {
        Console.WriteLine("[LegacyAdapter] Ресурсы освобождены");
        _telemetry.LogOperation("Adapter", "DisposeResources", TimeSpan.Zero);
    }

    public void Dispose()
    {
        DisposeResources();
        GC.SuppressFinalize(this);
    }
}
