namespace Patterns;

public class FluentDialog : IDialogRenderer
{
    public void Render(string title, string content)
    {
        Console.WriteLine($"  [Fluent] Диалог: {title} - {content}");
    }
}
