namespace Patterns;
using System.Collections.Generic;

public interface IContainerComponent : IUIComponent
{
    void AddChild(IUIComponent child);
    void RemoveChild(IUIComponent child);
    IReadOnlyList<IUIComponent> Children { get; }
}
