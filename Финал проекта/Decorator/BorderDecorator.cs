using System;
using Patterns;

namespace Patterns;

public class BorderDecorator : UIComponentDecorator {
    private Color _borderColor;
    private float _borderThickness;

    public BorderDecorator(IUIComponent component, Color borderColor, float thickness = 1.0f) 
        : base(component) {
        _borderColor = borderColor;
        _borderThickness = thickness;
    }

    public override void Render(IRenderingContext ctx) {
        var rect = _component.BoundingBox;
        
        _component.Render(ctx);
        
        Console.WriteLine($"[BorderDecorator] Отрисовка рамки вокруг ({rect.X},{rect.Y},{rect.Width},{rect.Height}) цветом rgb({_borderColor.R},{_borderColor.G},{_borderColor.B}) толщиной {_borderThickness}");
    }
}
