namespace Patterns;

public class StandardWidgetFactory : IWidgetFactory
{
    public IWidget CreateWidget(WidgetConfig config)
    {
        return config.Type switch
        {
            "Button" => new Button(config.Name),
            "TextBox" => new TextBox(config.Name),
            "Panel" => new Panel(config.Name),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };
    }
}
