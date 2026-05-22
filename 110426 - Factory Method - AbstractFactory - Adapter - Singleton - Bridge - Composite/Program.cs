using System;
using Patterns;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("CrossPlatformUISimulator\n");
        Console.WriteLine("1) Fluent Theme");
        Console.WriteLine("2) Cupertino Theme");
        Console.Write("Выберите тему: ");
        
        int choice = int.Parse(Console.ReadLine());
        
        IThemeFactory themeFactory = choice == 1
            ? new FluentThemeFactory()
            : new CupertinoThemeFactory();
        
        IWidgetFactory widgetFactory = new StandardWidgetFactory();
        
        IContainerBuilder builder = new UIContainerBuilder();
        
        var runner = new EndToEndIntegrationRunner(themeFactory, widgetFactory, builder);
        runner.Run();

        Tests.RunAll();
    }
}
