namespace Patterns;

public class CupertinoButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Cupertino: Кнопка");
    }
    public string GetStyle()
    {
        return "macOS (Cupertino)";
    }
}
