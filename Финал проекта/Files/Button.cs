namespace Patterns;

public class Button : IWidget
{
    private string name;
    public Button(string name)
    {
        this.name = name;
    }
    public void Render()
    {
        Console.WriteLine($"Button: {this.name}");
    }
}
