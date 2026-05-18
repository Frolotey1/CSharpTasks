namespace Patterns;

public class TextBox : IWidget
{
    private string name;
    public TextBox(string name)
    {
        this.name = name;
    }
    public void Render()
    {
        Console.WriteLine($"TextBox: {this.name}");
    }
}
