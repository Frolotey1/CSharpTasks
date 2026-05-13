using System;

namespace Patterns;

public class DebugWidgetFactory : IWidgetFactory
{
    public IWidget CreateWidget(WidgetConfig config)
    {
        Console.WriteLine($"Создание {config.Type}");

        var startTime = DateTime.UtcNow;

        IWidget widget = config.Type switch
        {
            "Button" => new Button(config.Name),
            "TextBox" => new TextBox(config.Name),
            "Panel" => new Panel(config.Name),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };

        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", $"CreateWidget.{config.Type}", duration, $"name={config.Name}");

        Console.WriteLine($"Создание завершено");
        return widget;
    }
}