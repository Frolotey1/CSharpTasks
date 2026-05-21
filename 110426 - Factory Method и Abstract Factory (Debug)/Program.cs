using System;
using Patterns;

public class Program {
    public static void Main() {
        Console.WriteLine("1) Fluent (Windows 11)");
        Console.WriteLine("2) Cupertino (macOS)");
        Console.Write("Выберите тему: ");
        int themeChoice = int.Parse(Console.ReadLine());

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

        var runner = new EndToEndScenarioRunner(themeFactory, widgetFactory);
        runner.Run();
    }
}
