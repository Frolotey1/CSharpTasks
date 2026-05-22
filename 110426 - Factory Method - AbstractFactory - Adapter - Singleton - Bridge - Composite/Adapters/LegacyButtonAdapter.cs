using System;

namespace Patterns;

public class LegacyButtonAdapter : IWidget
{
    private LegacyGraphicsEngine _legacyEngine;
    private string _text;
    private IApplicationTelemetry _telemetry;

    public LegacyButtonAdapter(LegacyGraphicsEngine legacyEngine, string text, IApplicationTelemetry telemetry)
    {
        _legacyEngine = legacyEngine;
        _text = text;
        _telemetry = telemetry;
    }

    public void Render()
    {
        var startTime = DateTime.UtcNow;
        _legacyEngine.DrawNativeButton(10, 10, 100, 40, _text);
        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Adapter", "Button.Render", duration, $"text={_text}");
    }
}
