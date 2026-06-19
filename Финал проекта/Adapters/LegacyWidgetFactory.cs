namespace Patterns;
using System;

public class LegacyWidgetFactory : IWidgetFactory
{
    private readonly LegacyGraphicsEngine _legacyEngine;
    private readonly IApplicationTelemetry _telemetry;

    public LegacyWidgetFactory(LegacyGraphicsEngine legacyEngine, IApplicationTelemetry telemetry)
    {
        _legacyEngine = legacyEngine;
        _telemetry = telemetry;
    }

    public IWidget CreateWidget(WidgetConfig config)
    {
        var startTime = DateTime.UtcNow;

        IWidget widget = config.Type switch
        {
            "Button" => new LegacyButtonAdapter(_legacyEngine, config.Name, _telemetry),
            "TextBox" => throw new NotSupportedException("LegacyGraphicsEngine не поддерживает TextBox"),
            "Panel" => throw new NotSupportedException("LegacyGraphicsEngine не поддерживает Panel"),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };

        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Factory", $"CreateWidget.{config.Type}", duration, $"name={config.Name}");

        return widget;
    }
}