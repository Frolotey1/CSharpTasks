namespace Patterns;

public class DebugWidgetFactory : IWidgetFactory
{
    public IWidget CreateWidget(WidgetConfig config)
    {
        Console.WriteLine($"Создание {config.Type}");
        
        IWidget widget = config.Type switch
        {
            "Button" => new Button(config.Name),
            "TextBox" => new TextBox(config.Name),
            "Panel" => new Panel(config.Name),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };
        
        Console.WriteLine($"Создание завершено");
        return widget;
    }
}
