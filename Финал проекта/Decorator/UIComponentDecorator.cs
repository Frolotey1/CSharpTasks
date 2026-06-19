using System;
using Patterns;

namespace Patterns;

public abstract class UIComponentDecorator : IUIComponent {
    protected readonly IUIComponent _component;

    protected UIComponentDecorator(IUIComponent component) {
        _component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public virtual string Id => _component.Id;
    public virtual Rectangle BoundingBox => _component.BoundingBox;

    public virtual void Render(IRenderingContext ctx) {
        _component.Render(ctx);
    }

    public virtual void SetPosition(Point position) {
        _component.SetPosition(position);
    }

    public virtual T FindById<T>(string id) where T : class, IUIComponent {
        return _component.FindById<T>(id);
    }
}
