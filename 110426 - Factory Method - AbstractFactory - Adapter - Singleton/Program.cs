using Patterns;

public class Program 
{
    public static void Main() 
    {

        var themeFactory = new FluentThemeFactory();
        var widgetFactory = new StandardWidgetFactory();
        var runner = new EndToEndScenarioRunner(themeFactory, widgetFactory);
        runner.Run();
       
    }
}
