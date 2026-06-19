using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Mediator;

public class EventDrivenMediator : IUIComponentMediator
{
    private readonly ConcurrentDictionary<string, List<Subscription>> _subscriptions = new();
    private readonly IApplicationTelemetry _telemetry;
    private readonly object _lock = new object();

    public EventDrivenMediator(IApplicationTelemetry telemetry)
    {
        _telemetry = telemetry;
    }

    public void Subscribe(string eventType, Action<IUIComponent, UIEvent> handler)
    {
        if (string.IsNullOrEmpty(eventType))
            throw new ArgumentException("Event type cannot be empty");
        
        var subscriptions = _subscriptions.GetOrAdd(eventType, _ => new List<Subscription>());
        
        lock (_lock)
        {
            var dummy = new MediatorDummyComponent();
            subscriptions.Add(new Subscription(null, dummy, handler));
        }
        
        _telemetry.LogOperation("Mediator", "Subscribe", TimeSpan.Zero, $"EventType={eventType}");
    }

    public void Subscribe(string eventType, Predicate<UIEvent> filter, Action<IUIComponent, UIEvent> handler)
    {
        if (string.IsNullOrEmpty(eventType))
            throw new ArgumentException("Event type cannot be empty");
        
        var subscriptions = _subscriptions.GetOrAdd(eventType, _ => new List<Subscription>());
        
        lock (_lock)
        {
            var dummy = new MediatorDummyComponent();
            subscriptions.Add(new Subscription(filter, dummy, handler));
        }
        
        _telemetry.LogOperation("Mediator", "SubscribeFiltered", TimeSpan.Zero, $"EventType={eventType}");
    }

    public void Unsubscribe(Action<IUIComponent, UIEvent> handler)
    {
        lock (_lock)
        {
            foreach (var kvp in _subscriptions)
            {
                kvp.Value.RemoveAll(s => s.Handler == handler);
            }
        }
        _telemetry.LogOperation("Mediator", "Unsubscribe", TimeSpan.Zero, null);
    }

    public void Notify(IUIComponent sender, UIEvent uiEvent)
    {
        if (!_subscriptions.TryGetValue(uiEvent.EventType, out var subscriptions))
            return;
        
        List<Subscription> snapshot;
        lock (_lock)
        {
            snapshot = subscriptions.ToList();
        }
        
        int delivered = 0;
        int filtered = 0;
        
        foreach (var sub in snapshot)
        {
            if (!sub.IsAlive)
                continue;
            
            filtered++;
            if (sub.Filter == null || sub.Filter(uiEvent))
            {
                delivered++;
                try
                {
                    sub.Handler(sender, uiEvent);
                }
                catch (Exception ex)
                {
                    _telemetry.LogError("Mediator", $"Handler error: {ex.Message}");
                }
            }
        }
        
        _telemetry.LogOperation("Mediator", "Notify", TimeSpan.Zero, 
            $"Event={uiEvent.EventType}, Delivered={delivered}, Filtered={filtered}");
    }

    public void CleanupDeadSubscriptions()
    {
        lock (_lock)
        {
            foreach (var kvp in _subscriptions)
            {
                kvp.Value.RemoveAll(s => !s.IsAlive);
            }
        }
    }

    private class MediatorDummyComponent : IUIComponent
    {
        public string Id => "__mediator_dummy__";
        public Rectangle BoundingBox => new Rectangle();
        public void Render(IRenderingContext ctx) { }
        public void SetPosition(Point position) { }
        public T FindById<T>(string id) where T : class, IUIComponent => null;
        public void SetMediator(IUIComponentMediator mediator) { }
        public void SendEvent(UIEvent uiEvent) { }
        public void Subscribe(string eventType, Action<IUIComponent, UIEvent> handler) { }
        public void Unsubscribe(string eventType, Action<IUIComponent, UIEvent> handler) { }
    }
}
