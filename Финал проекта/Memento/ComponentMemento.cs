using System;

namespace Patterns.Memento;

internal class ComponentMemento : IMemento
{
    public string ComponentId { get; }
    public Point Position { get; }
    public string Text { get; }
    public bool IsVisible { get; }
    public string StyleKeyId { get; }
    public string StateName { get; }
    public DateTime CreatedAt { get; }

    public ComponentMemento(string componentId, Point position, string text, bool isVisible, string styleKeyId, string stateName)
    {
        ComponentId = componentId;
        Position = position;
        Text = text;
        IsVisible = isVisible;
        StyleKeyId = styleKeyId;
        StateName = stateName;
        CreatedAt = DateTime.UtcNow;
    }
}

public class MementoIncompatibleException : Exception
{
    public MementoIncompatibleException(string message) : base(message) { }
}
