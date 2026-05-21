namespace Patterns;
using System;

using Patterns;

public class LegacyEngineRenderingAdapter : IDialogRenderer {
    private readonly LegacyGraphicsEngine _legacyEngine;
    private readonly IApplicationTelemetry _telemetry;

    public LegacyEngineRenderingAdapter(LegacyGraphicsEngine legacyEngine, IApplicationTelemetry telemetry) {
        _legacyEngine = legacyEngine;
        _telemetry = telemetry;
    }

    public void Render(string title, string content) {
        var startTime = DateTime.UtcNow;

        try {
            _legacyEngine.InitializeRawContext(IntPtr.Zero);
            _legacyEngine.RenderTextRaster("Arial", 14, 0, 0, 0, 10, 10, title);
            _legacyEngine.RenderTextRaster("Arial", 12, 100, 100, 100, 10, 40, content);
            _legacyEngine.DrawNativeButton(10, 70, 80, 30, "OK");
            _legacyEngine.ShowModalWindow(IntPtr.Zero, title, true);

            var duration = DateTime.UtcNow - startTime;
            _telemetry.LogOperation("Adapter", "Render", duration, $"title={title}");
        }
        catch (Exception ex) {
            var duration = DateTime.UtcNow - startTime;
            _telemetry.LogOperation("Adapter", "Render.Error", duration, ex.Message);
            throw;
        }
    }
}
