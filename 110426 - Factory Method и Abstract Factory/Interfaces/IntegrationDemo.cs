namespace Patterns;

public class IntegrationDemo
{
    public static void Run()
    {
        Console.Write("1) Fluent (Windows 11)\n2) Cupertino (macOS)\nВыберите тему: ");
        int themeChoice = int.Parse(Console.ReadLine());

        if (themeChoice != 1 && themeChoice != 2)
        {
            Console.WriteLine("Нет такой темы");
            return;
        }
        
        IThemeFactory themeFactory = themeChoice == 1 
            ? new FluentThemeFactory() 
            : new CupertinoThemeFactory();
        
        Console.WriteLine("Выберите фабрику виджетов:");
        Console.WriteLine("1) StandardWidgetFactory");
        Console.WriteLine("2) DebugWidgetFactory");
        Console.Write("Выберите фабрику: ");
        int widgetChoice = int.Parse(Console.ReadLine());
        
        IWidgetFactory widgetFactory = widgetChoice == 1
            ? new StandardWidgetFactory()
            : new DebugWidgetFactory();
        
        var app = new ApplicationHost(themeFactory, widgetFactory);
        app.BuildTestWindow();
    }
}
