using Patterns;
namespace Patterns;

public interface ILazyComponentProxy : IUIComponent {
    bool IsMaterialized { get; }
    void Materialize();
    IUIComponent GetRealSubject();
}
