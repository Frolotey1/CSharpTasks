using System;

namespace Patterns.TemplateMethod;

public class StandardComponentLifecycle : UIComponentLifecycleBase
{
    private readonly IUIComponent _component;

    public StandardComponentLifecycle(IUIComponent component)
    {
        _component = component;
    }

    protected override void Initialize(UIContext ctx)
    {
        ctx.Telemetry.LogOperation("Lifecycle", "Initialize", TimeSpan.Zero, _component.Id);
    }

    protected override bool Validate(UIContext ctx)
    {
        return _component.BoundingBox.Width > 0 && _component.BoundingBox.Height > 0;
    }

    protected override void Render(UIContext ctx)
    {
        _component.Render(ctx.RenderingContext);
    }
}
