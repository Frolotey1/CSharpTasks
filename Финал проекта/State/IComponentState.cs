using Patterns;

namespace Patterns.State;

public interface IComponentState
{
    string StateName { get; }
    void Enter(IUIComponent context);
    void Exit(IUIComponent context);
    void HandleClick(IUIComponent context);
    void HandleRender(IUIComponent context, IRenderingContext ctx);
}
