namespace Patterns;

public class FluentCheckBox : ICheckBox
{
    public void Render()
    {
        Console.WriteLine("Fluent: Чекбокс");
    }
    public string GetStyle()
    {
        return "Windows 11 (Fluent)";
    }
}
