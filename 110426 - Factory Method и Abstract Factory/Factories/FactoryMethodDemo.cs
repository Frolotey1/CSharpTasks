namespace Patterns;

public class FactoryMethodDemo
{
    public static void Run()
    {
        Console.Write("1) StandardWidgetFactory\n2) DebugWidgetFactory\nВыберите фабрику: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice != 1 && choice != 2)
        {
            Console.WriteLine("Такой фабрики нет");
            return;
        }
        
        IWidgetFactory factory = choice == 1 
            ? new StandardWidgetFactory() 
            : new DebugWidgetFactory();
        
        var configs = new[]
        {
            new WidgetConfig { Type = "Button", Name = "OK" },
            new WidgetConfig { Type = "TextBox", Name = "Ввод" },
            new WidgetConfig { Type = "Panel", Name = "Панель" }
        };
        
        foreach (var cfg in configs)
        {
            var widget = factory.CreateWidget(cfg);
            widget.Render();
        }
    }
}
