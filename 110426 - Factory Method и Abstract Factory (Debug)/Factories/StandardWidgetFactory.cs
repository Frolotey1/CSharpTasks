namespace Patterns;
using System;


public class StandardWidgetFactory : IWidgetFactory {
    public IWidget CreateWidget(WidgetConfig config) {
        var startTime = DateTime.UtcNow;

        IWidget widget = config.Type switch {
            "Button" => new Button(config.Name),
            "TextBox" => new TextBox(config.Name),
            "Panel" => new Panel(config.Name),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };

        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", $"CreateWidget.{config.Type}", duration, $"name={config.Name}");

        return widget;
    }
}
