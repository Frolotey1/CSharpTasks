namespace Patterns;

public class AbstractFactoryDemo
{
    public static void Run()
    {
        Console.Write("1) Fluent (Windows 11)\n2) Cupertino (macOS)\nВыберите тему: ");
        int choiceTheme = int.Parse(Console.ReadLine());

        if (choiceTheme != 1 && choiceTheme != 2)
        {
            Console.WriteLine("Такой темы нет");
            return;
        }
        
        IThemeFactory factory = choiceTheme == 1
            ? new FluentThemeFactory() 
            : new CupertinoThemeFactory();
        
        Console.WriteLine($"\nТема: {factory.ThemeName}\n");
        
        var button = factory.CreateButton();
        button.Render();
        Console.WriteLine($"Стиль: {button.GetStyle()}");
        
        var checkbox = factory.CreateCheckBox();
        checkbox.Render();
        Console.WriteLine($"Стиль: {checkbox.GetStyle()}");
        
        var dialog = factory.CreateDialogRenderer();
        dialog.Render("Заголовок", "Содержимое");
    }
}
