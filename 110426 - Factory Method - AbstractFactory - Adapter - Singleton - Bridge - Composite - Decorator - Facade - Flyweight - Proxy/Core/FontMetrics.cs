namespace Patterns;

public struct FontMetrics
{
    public string FontFamily { get; set; }
    public int Size { get; set; }
    public bool Bold { get; set; }
    public bool Italic { get; set; }

    public FontMetrics(string fontFamily, int size, bool bold = false, bool italic = false)
    {
        FontFamily = fontFamily;
        Size = size;
        Bold = bold;
        Italic = italic;
    }
}
