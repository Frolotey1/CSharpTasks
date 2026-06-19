using System;

namespace Patterns;

public class LegacyGraphicsEngine
{
    public void InitializeRawContext(IntPtr windowHandle)
    {
        Console.WriteLine($"[Legacy] InitializeRawContext: {windowHandle}");
    }

    public void DrawNativeButton(int x, int y, int width, int height, string legacyLabel)
    {
        Console.WriteLine($"[Legacy] DrawNativeButton: ({x},{y},{width},{height}) label='{legacyLabel}'");
    }

    public void RenderTextRaster(string fontName, int size, int r, int g, int b, int x, int y, string text)
    {
        Console.WriteLine($"[Legacy] RenderTextRaster: '{text}' at ({x},{y}) font={fontName} size={size} color=({r},{g},{b})");
    }

    public void ShowModalWindow(IntPtr parent, string title, bool blockInput)
    {
        Console.WriteLine($"[Legacy] ShowModalWindow: title='{title}'");
    }
}
