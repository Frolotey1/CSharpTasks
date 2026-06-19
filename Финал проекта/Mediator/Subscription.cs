using System;

namespace Patterns.Mediator;

public class Subscription
{
    public Predicate<UIEvent> Filter { get; }
    public WeakReference<IUIComponent> Target { get; }
    public Action<IUIComponent, UIEvent> Handler { get; }

    public Subscription(Predicate<UIEvent> filter, IUIComponent target, Action<IUIComponent, UIEvent> handler)
    {
        Filter = filter;
        Target = new WeakReference<IUIComponent>(target);
        Handler = handler;
    }

    public bool IsAlive => Target.TryGetTarget(out _);
}
