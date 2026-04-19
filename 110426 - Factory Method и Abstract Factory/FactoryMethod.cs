namespace Patterns;
using System;

public interface IWidget {
    void Render();
}

public class Button : IWidget {
    private string name;
    public Button(string name) {
        this.name = name;
    }
    public void Render() {
        Console.WriteLine($"Button: {this.name}");
    }
}

public class TextBox : IWidget {
    private string name;
    public TextBox(string name) {
        this.name = name;
    }
    public void Render() {
        Console.WriteLine($"TextBox: {this.name}");
    }
}

public class Panel : IWidget {
    private string name;
    public Panel(string name) {
        this.name = name;
    }
    public void Render() {
        Console.WriteLine($"Panel: {this.name}");
    }
}

public class WidgetConfig {
    public required string Type { get; init; }
    public required string Name { get; init; }
}

public interface IWidgetFactory {
    IWidget CreateWidget(WidgetConfig config);
}

public class StandardWidgetFactory : IWidgetFactory {
    public IWidget CreateWidget(WidgetConfig config) {
        switch(config.Type) {
            case "Button":
                return new Button(config.Name);
            case "TextBox":
                return new TextBox(config.Name);
            case "Panel":
                return new Panel(config.Name);
            default:
                throw new Exception($"Неизвестный тип: {config.Type}");
        }
    }
}

public class DebugWidgetFactory : IWidgetFactory {
    public IWidget CreateWidget(WidgetConfig config) {
        Console.WriteLine($"[DEBUG] Создание {config.Type}...");
        
        IWidget widget = config.Type switch {
            "Button" => new Button(config.Name),
            "TextBox" => new TextBox(config.Name),
            "Panel" => new Panel(config.Name),
            _ => throw new Exception($"Неизвестный тип: {config.Type}")
        };
        
        Console.WriteLine($"[DEBUG] Создание завершено");
        return widget;
    }
}

public class FactoryMethodDemo {
    public static void Run() {
        Console.Write("1) StandardWidgetFactory\n2) DebugWidgetFactory\nВыберите фабрику: ");
        int choice = int.Parse(Console.ReadLine());

	if(choice != 1 && choice != 2) {
	    Console.WriteLine("Такой фабрики нет");
	    return;
	}
        
        IWidgetFactory factory = choice == 1 
            ? new StandardWidgetFactory() 
            : new DebugWidgetFactory();
        
        var configs = new[] {
            new WidgetConfig { Type = "Button", Name = "OK" },
            new WidgetConfig { Type = "TextBox", Name = "Ввод" },
            new WidgetConfig { Type = "Panel", Name = "Панель" }
        };
        
        foreach (var cfg in configs) {
            var widget = factory.CreateWidget(cfg);
            widget.Render();
        }
    }
}
