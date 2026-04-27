namespace Patterns;

public class FluentButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Fluent: Кнопка");
    }
    public string GetStyle()
    {
        return "Windows 11 (Fluent)";
    }
}
