namespace Patterns;

public class CupertinoFont : IFontEngine
{
    private string font = "San Francisco";
    public void SetFont(string font) { this.font = font; }
    public string GetFont() { return this.font; }
}
