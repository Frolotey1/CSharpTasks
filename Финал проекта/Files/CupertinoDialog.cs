namespace Patterns;

public class CupertinoDialog : IDialogRenderer
{
    public void Render(string title, string content)
    {
        Console.WriteLine($"Cupertino: Диалог: {title} - {content}");
    }
}
