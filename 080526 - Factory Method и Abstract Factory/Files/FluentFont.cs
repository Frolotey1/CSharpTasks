namespace Patterns;

public class FluentFont : IFontEngine
{
    private string font = "Segoe UI";
    public void SetFont(string font)
    {
        this.font = font;
    }
    public string GetFont() { return font; }
}
