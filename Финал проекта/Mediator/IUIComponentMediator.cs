using System;

namespace Patterns.Mediator;

public record UIEvent(Guid SenderId, DateTime Timestamp, string EventType, object Payload);

public interface IUIComponentMediator
{
    void Subscribe(string eventType, Action<IUIComponent, UIEvent> handler);
    void Subscribe(string eventType, Predicate<UIEvent> filter, Action<IUIComponent, UIEvent> handler);
    void Unsubscribe(Action<IUIComponent, UIEvent> handler);
    void Notify(IUIComponent sender, UIEvent uiEvent);
}
