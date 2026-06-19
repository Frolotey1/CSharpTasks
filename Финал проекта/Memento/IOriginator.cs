using Patterns.Memento;

namespace Patterns;

public interface IOriginator
{
    IMemento CreateMemento();
    void Restore(IMemento memento);
}
