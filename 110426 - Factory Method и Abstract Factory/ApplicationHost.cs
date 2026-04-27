namespace Patterns;

public class ApplicationHost
{
    private IThemeFactory _themeFactory;
    private IWidgetFactory _widgetFactory;

    public ApplicationHost(IThemeFactory themeFactory, IWidgetFactory widgetFactory)
    {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
    }

    public void BuildTestWindow()
    {
        var dialog = _themeFactory.CreateDialogRenderer();
        dialog.Render("Тестовое окно", "Привет, мир!");
        
        var button = _themeFactory.CreateButton();
        button.Render();
        
        var checkbox = _themeFactory.CreateCheckBox();
        checkbox.Render();
       
        var font = _themeFactory.CreateFontEngine();
        font.SetFont("Arial");
        Console.WriteLine($"Шрифт: {font.GetFont()}");
        
        var widgets = new[]
        {
            new WidgetConfig { Type = "Button", Name = "OK" },
            new WidgetConfig { Type = "TextBox", Name = "Ввод" }
        };
        
        foreach (var cfg in widgets)
        {
            var widget = _widgetFactory.CreateWidget(cfg);
            widget.Render();
        }
    }
}
