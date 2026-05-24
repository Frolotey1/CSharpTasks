using Patterns;
using System;
using System.Collections.Generic;

public abstract class UIComponentBase : IUIComponent
{
    public string Id { get; protected set; }
    public Rectangle BoundingBox { get; protected set; }
    protected IRenderingStrategy _renderingStrategy;

    protected UIComponentBase(string id, IRenderingStrategy strategy)
    {
        Id = id;
        _renderingStrategy = strategy;
        BoundingBox = new Rectangle(0, 0, 100, 50);
    }

    public virtual void Render(IRenderingContext ctx)
    {
        _renderingStrategy.DrawBackground(BoundingBox, Color.White);
        _renderingStrategy.DrawBorder(BoundingBox, Color.Black, 1);
    }

    public virtual void SetPosition(Point position)
    {
        BoundingBox = new Rectangle(position.X, position.Y, BoundingBox.Width, BoundingBox.Height);
    }

    public void SwitchRenderingStrategy(IRenderingStrategy newStrategy)
    {
        _renderingStrategy = newStrategy;
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Bridge", "SwitchStrategy", TimeSpan.Zero, $"ComponentId={Id}");
    }

    public abstract T FindById<T>(string id) where T : class, IUIComponent;

    protected IUIStyleFlyweight? _style;
    
    public void SetStyle(IUIStyleFlyweight style) {
        _style = style;
    }
    
}
