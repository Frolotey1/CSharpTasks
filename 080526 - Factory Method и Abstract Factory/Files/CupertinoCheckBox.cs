namespace Patterns;

public class CupertinoCheckBox : ICheckBox
{
    public void Render()
    {
        Console.WriteLine("Cupertino: Чекбокс");
    }
    public string GetStyle()
    {
        return "macOS (Cupertino)";
    }
}
