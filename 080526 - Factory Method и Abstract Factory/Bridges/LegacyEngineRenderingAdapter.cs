using System;

namespace Patterns;

public class LegacyEngineRenderingAdapter : IRenderingStrategy
{
    private LegacyGraphicsEngine _legacyEngine;
    private IApplicationTelemetry _telemetry;

    public LegacyEngineRenderingAdapter(LegacyGraphicsEngine legacyEngine, IApplicationTelemetry telemetry)
    {
        _legacyEngine = legacyEngine;
        _telemetry = telemetry;
    }

    public void DrawBackground(Rectangle rect, Color fill)
    {
        var startTime = DateTime.UtcNow;
        _legacyEngine.InitializeRawContext(IntPtr.Zero);
        _legacyEngine.RenderTextRaster("Arial", 14, fill.R, fill.G, fill.B, rect.X, rect.Y, "Background");
        _telemetry.LogOperation("Adapter", "DrawBackground", DateTime.UtcNow - startTime);
    }

    public void DrawBorder(Rectangle rect, Color stroke, float thickness)
    {
        Console.WriteLine($"[LegacyAdapter] DrawBorder not fully supported");
    }

    public void DrawText(string text, FontMetrics font, Point position, Color color)
    {
        _legacyEngine.RenderTextRaster(font.FontFamily, font.Size, color.R, color.G, color.B, position.X, position.Y, text);
    }

    public bool HitTest(Rectangle bounds, Point cursor)
    {
        return bounds.Contains(cursor);
    }

    public void DisposeResources()
    {
        Console.WriteLine("[LegacyAdapter] Resources disposed");
    }

    public void Dispose()
    {
        DisposeResources();
        GC.SuppressFinalize(this);
    }
}
