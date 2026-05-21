namespace Patterns;

using System;
using System.Collections.Generic;
using System.Linq;

public class PanelComponent : UIComponentBase, IContainerComponent
{
    private List<IUIComponent> _children = new List<IUIComponent>();
    private HashSet<string> _childIds = new HashSet<string>();

    public IReadOnlyList<IUIComponent> Children => _children.AsReadOnly();

    public PanelComponent(string id, IRenderingStrategy strategy) : base(id, strategy)
    {
        BoundingBox = new Rectangle(0, 0, 400, 300);
    }

    public void AddChild(IUIComponent child)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child));

        if (IsCyclicReference(child))
            throw new InvalidOperationException("Циклическая ссылка запрещена");

        if (_childIds.Contains(child.Id))
            throw new InvalidOperationException($"Дублирование Id: {child.Id}");

        _children.Add(child);
        _childIds.Add(child.Id);
    }

    public void RemoveChild(IUIComponent child)
    {
        if (_children.Remove(child))
        {
            _childIds.Remove(child.Id);
        }
    }

    private bool IsCyclicReference(IUIComponent child)
    {
        if (child == this) return true;

        if (child is IContainerComponent container)
        {
            foreach (var descendant in GetAllDescendants(container))
            {
                if (descendant == this) return true;
            }
        }
        return false;
    }

    private IEnumerable<IUIComponent> GetAllDescendants(IContainerComponent container)
    {
        var stack = new Stack<IUIComponent>();
        foreach (var child in container.Children)
        {
            stack.Push(child);
        }

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            yield return current;
            if (current is IContainerComponent cont)
            {
                foreach (var child in cont.Children)
                {
                    stack.Push(child);
                }
            }
        }
    }

    public override void Render(IRenderingContext ctx)
    {
        base.Render(ctx);
        foreach (var child in _children)
        {
            child.Render(ctx);
        }
    }

    public override T FindById<T>(string id)
    {
        if (Id == id && this is T result)
            return result;

        foreach (var child in _children)
        {
            var found = child.FindById<T>(id);
            if (found != null)
                return found;
        }
        return null;
    }
}
