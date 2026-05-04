namespace Patterns;

public class IntegrationDemo {
    public static void Run() {
        System.Console.Write("1) Fluent (Windows 11)\n2) Cupertino (macOS)\nВыберите тему: ");
        int themeChoice = int.Parse(System.Console.ReadLine());

        if (themeChoice != 1 && themeChoice != 2) {
            System.Console.WriteLine("Нет такой темы");
            return;
        }
        
        IThemeFactory themeFactory = themeChoice == 1 
            ? new FluentThemeFactory() 
            : new CupertinoThemeFactory();
        
        System.Console.WriteLine("Выберите фабрику виджетов:");
        System.Console.WriteLine("1) StandardWidgetFactory");
        System.Console.WriteLine("2) DebugWidgetFactory");
        System.Console.Write("Выберите фабрику: ");
        int widgetChoice = int.Parse(Console.ReadLine());
        
        IWidgetFactory widgetFactory = widgetChoice == 1
            ? new StandardWidgetFactory()
            : new DebugWidgetFactory();
        
        System.Console.WriteLine("Демонстрация Abstract Factory + Factory Method");
        var app = new ApplicationHost(themeFactory, widgetFactory);
        app.BuildTestWindow();
        
        System.Console.WriteLine("Демонстрация Builder + Prototype");
        var runner = new UIScenarioRunner(themeFactory, widgetFactory);
        runner.Run();
    }
}
