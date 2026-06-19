using System.Collections.Generic;

namespace Patterns;

public enum StackDirection
{
    Vertical,
    Horizontal
}

public class StackLayoutStrategy : ILayoutStrategy
{
    private readonly StackDirection _direction;

    public StackLayoutStrategy(StackDirection direction = StackDirection.Vertical)
    {
        _direction = direction;
    }

    public IReadOnlyDictionary<IUIComponent, Rectangle> CalculateBounds(IContainerComponent container, LayoutContext context)
    {
        var result = new Dictionary<IUIComponent, Rectangle>();
        var currentPosition = context.Padding;
        var containerRect = container.BoundingBox;
        
        foreach (var child in container.Children)
        {
            var childSize = child.BoundingBox;
            Rectangle bounds;
            
            if (_direction == StackDirection.Vertical)
            {
                bounds = new Rectangle(
                    containerRect.X + context.Padding,
                    containerRect.Y + currentPosition,
                    containerRect.Width - context.Padding * 2,
                    childSize.Height);
                currentPosition += childSize.Height + context.Spacing;
            }
            else
            {
                bounds = new Rectangle(
                    containerRect.X + currentPosition,
                    containerRect.Y + context.Padding,
                    childSize.Width,
                    containerRect.Height - context.Padding * 2);
                currentPosition += childSize.Width + context.Spacing;
            }
            
            result[child] = bounds;
        }
        
        return result;
    }
}
