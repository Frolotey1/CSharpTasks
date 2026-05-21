using System;
using Patterns;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Главное меню");
        Console.WriteLine("1) Factory Method");
        Console.WriteLine("2) Abstract Factory");
        Console.WriteLine("3) Integration (Bridge + Composite + Adapter + Singleton + Bridge + Composite)");
        Console.WriteLine("4) Запустить тесты");
        Console.WriteLine("5) Выход");
        Console.Write("Выберите опцию: ");

        int selectOption = int.Parse(Console.ReadLine());

        switch (selectOption)
        {
            case 1:
                FactoryMethodDemo.Run();
                break;
            case 2:
                AbstractFactoryDemo.Run();
                break;
            case 3:
                RunIntegration();
                break;
            case 4:
                SimpleTests.RunAll();
                break;
            case 5:
                Console.WriteLine("Завершение программы");
                return;
            default:
                Console.WriteLine("Нет такой опции");
                break;
        }
    }

    private static void RunIntegration()
    {
        IThemeFactory themeFactory = new FluentThemeFactory();
        IWidgetFactory widgetFactory = new StandardWidgetFactory();
        IContainerBuilder builder = new UIContainerBuilder();

        var runner = new EndToEndIntegrationRunner(themeFactory, widgetFactory, builder);
        runner.Run();
    }
}
