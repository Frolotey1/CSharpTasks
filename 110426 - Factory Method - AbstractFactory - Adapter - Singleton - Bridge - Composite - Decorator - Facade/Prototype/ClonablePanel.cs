using System.Collections.Generic;

namespace Patterns;

public class ClonablePanel : PanelComponent, IPrototypical<ClonablePanel>
{
    public ClonablePanel(string id, IRenderingStrategy strategy) : base(id, strategy)
    {
    }

    public new ClonablePanel Clone()
    {
        var clone = new ClonablePanel(Id + "_clone", _renderingStrategy);
        clone.SetPosition(new Point(BoundingBox.X, BoundingBox.Y));
        
        foreach (var child in Children)
        {
            if (child is ClonableButtonComponent btn)
            {
                clone.AddChild(btn.Clone());
            }
            else if (child is ButtonComponent button)
            {
                clone.AddChild(new ButtonComponent(button.Id + "_clone", _renderingStrategy, button.Text));
            }
            else if (child is IUIComponent comp)
            {
                clone.AddChild(comp);
            }
        }
        
        return clone;
    }
}
