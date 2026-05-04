using System;

namespace Patterns;

public class UIScenarioRunner
{
    private IThemeFactory _themeFactory;
    private IWidgetFactory _widgetFactory;
    private static int _factoryCallCount = 0;
    private static int _builderCallCount = 0;
    private static int _cloneCallCount = 0;
    
    public UIScenarioRunner(IThemeFactory themeFactory, IWidgetFactory widgetFactory) {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
    }
    
    public void Run() {
        Console.WriteLine("=== Сценарий 1: Создание диалога через Director + Builder ===");
        
        DialogBuilder builder = new DialogBuilder(_widgetFactory);
        builder.ConfigureTheme(_themeFactory);
        
        ErrorDialogDirector director = new ErrorDialogDirector(builder);
        IDialog dialog = director.Construct();
        dialog.Render();
        
        _builderCallCount++;
        
        Console.WriteLine("\n=== Сценарий 2: Создание клона и модификация ===");
        
        ClonableDialog original = new ClonableDialog();
        original.Title = "Шаблон диалога";
        original.Icon = "template.png";
        original.Buttons.Add(new ClonableButton("Default", "OK"));
        original.Buttons.Add(new ClonableButton("Default", "Cancel"));
        
        ClonableDialog clone = original.Clone();
        clone.Title = "Модифицированный диалог";
        clone.Buttons[0] = new ClonableButton("Default", "Применить");
        
        _cloneCallCount++;
        
        Console.WriteLine("Оригинал:");
        original.Render();
        Console.WriteLine("Клон после модификации:");
        clone.Render();
        
        Console.WriteLine("\n=== Сценарий 3: Внедрение кастомного виджета в клон ===");
        
        IWidget customWidget = _widgetFactory.CreateWidget(new WidgetConfig { Type = "Button", Name = "Custom Button" });
        clone.CustomWidgets.Add(customWidget);
        _factoryCallCount++;
        
        Console.WriteLine("Клон с кастомным виджетом:");
        clone.Render();
        
        Console.WriteLine("\n=== Сценарий 4: Демонстрация экономии от прототипа ===");
        Console.WriteLine("Количество вызовов фабрик: " + _factoryCallCount);
        Console.WriteLine("Количество вызовов Builder: " + _builderCallCount);
        Console.WriteLine("Количество клонирований: " + _cloneCallCount);
        
        int totalObjects = _factoryCallCount + _builderCallCount + _cloneCallCount;
        int savedByClone = _cloneCallCount * 3;
        Console.WriteLine("Без прототипа потребовалось бы создать ~" + (totalObjects + savedByClone) + " объектов");
        Console.WriteLine("Прототип сэкономил " + savedByClone + " операций создания");
    }
}
