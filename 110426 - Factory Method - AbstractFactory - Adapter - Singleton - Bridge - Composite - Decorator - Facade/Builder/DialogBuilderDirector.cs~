namespace Patterns;

public class DialogBuilderDirector
{
    private IContainerBuilder _builder;
    private IWidgetFactory _widgetFactory;
    private IThemeFactory _themeFactory;

    public DialogBuilderDirector(IContainerBuilder builder, IWidgetFactory widgetFactory, IThemeFactory themeFactory)
    {
        _builder = builder;
        _widgetFactory = widgetFactory;
        _themeFactory = themeFactory;
    }

    public IContainerComponent Construct()
    {
        _builder.ConfigureTheme(_themeFactory);
        _builder.SetTitle("Тестовый диалог");
        _builder.AddButton(new ButtonConfig("OK", true));
        _builder.AddButton(new ButtonConfig("Cancel"));
        return _builder.Build();
    }
}
