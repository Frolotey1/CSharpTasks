using System;

namespace Patterns.TemplateMethod;

public class ComplexContainerLifecycle : UIComponentLifecycleBase
{
    private readonly IContainerComponent _container;

    public ComplexContainerLifecycle(IContainerComponent container)
    {
        _container = container;
    }

    protected override void Initialize(UIContext ctx)
    {
        ctx.Telemetry.LogOperation("Lifecycle", "InitializeContainer", TimeSpan.Zero, _container.Id);
    }

    protected override void LoadResources(UIContext ctx)
    {
        foreach (var child in _container.Children)
        {
            var childLifecycle = new StandardComponentLifecycle(child);
            childLifecycle.ExecuteLifecycle(ctx);
        }
    }

    protected override bool Validate(UIContext ctx)
    {
        foreach (var child in _container.Children)
        {
            if (child.BoundingBox.Width <= 0 || child.BoundingBox.Height <= 0)
                return false;
        }
        return true;
    }

    protected override void Render(UIContext ctx)
    {
        _container.Render(ctx.RenderingContext);
    }
}
