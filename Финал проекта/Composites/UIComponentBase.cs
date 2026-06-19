using System;
using System.Collections.Generic;
using Patterns.Mediator;
using Patterns.Memento;
using Patterns.Visitor;

namespace Patterns;

public abstract class UIComponentBase : IUIComponent, IAcceptable, IOriginator
{
    public string Id { get; protected set; }
    public Rectangle BoundingBox { get; protected set; }
    protected IRenderingStrategy _renderingStrategy;
    protected IUIStyleFlyweight _style = null;
    protected IUIComponentMediator _mediator = null;
    protected Dictionary<string, List<Action<IUIComponent, UIEvent>>> _subscriptions = new();

    protected UIComponentBase(string id, IRenderingStrategy strategy)
    {
        Id = id;
        _renderingStrategy = strategy;
        BoundingBox = new Rectangle(0, 0, 100, 50);
    }

    public virtual void SetMediator(IUIComponentMediator mediator)
    {
        _mediator = mediator;
    }

    public virtual void SendEvent(UIEvent uiEvent)
    {
        _mediator?.Notify(this, uiEvent);
    }

    public virtual void Subscribe(string eventType, Action<IUIComponent, UIEvent> handler)
    {
        if (!_subscriptions.ContainsKey(eventType))
            _subscriptions[eventType] = new List<Action<IUIComponent, UIEvent>>();
        _subscriptions[eventType].Add(handler);
        _mediator?.Subscribe(eventType, handler);
    }

    public virtual void Unsubscribe(string eventType, Action<IUIComponent, UIEvent> handler)
    {
        if (_subscriptions.TryGetValue(eventType, out var handlers))
        {
            handlers.Remove(handler);
            _mediator?.Unsubscribe(handler);
        }
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

    public virtual void Accept(IUIComponentVisitor visitor)
    {
    }

    public void SwitchRenderingStrategy(IRenderingStrategy newStrategy)
    {
        _renderingStrategy = newStrategy;
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Bridge", "SwitchStrategy", TimeSpan.Zero, $"ComponentId={Id}");
    }

    public abstract T FindById<T>(string id) where T : class, IUIComponent;

    public IUIStyleFlyweight GetStyle() => _style;

    public void SetStyle(IUIStyleFlyweight style)
    {
        _style = style;
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Flyweight", "SetStyle", TimeSpan.Zero, $"ComponentId={Id}");
    }

    public virtual Patterns.Memento.IMemento CreateMemento()
    {
	return new ComponentMemento(Id, new Point(BoundingBox.X, BoundingBox.Y), null, true, _style?.StyleId.ToString() ?? "", "Normal");
    }

    public virtual void Restore(Patterns.Memento.IMemento memento)
    {
        if (memento is ComponentMemento cm)
        {
            SetPosition(cm.Position);
        }
    }
}
