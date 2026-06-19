using System.Collections.Generic;

namespace Patterns;

public interface ILayoutStrategy
{
    IReadOnlyDictionary<IUIComponent, Rectangle> CalculateBounds(
        IContainerComponent container,
        LayoutContext context);
}
