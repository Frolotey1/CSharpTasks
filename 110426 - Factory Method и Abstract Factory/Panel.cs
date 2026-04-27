namespace Patterns;

public class Panel : IWidget
{
    private string name;
    public Panel(string name)
    {
        this.name = name;
    }
    public void Render()
    {
        Console.WriteLine($"Panel: {this.name}");
    }
}
