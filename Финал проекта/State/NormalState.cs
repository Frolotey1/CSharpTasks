using System;

namespace Patterns.State;

public class NormalState : IComponentState
{
    public string StateName => "Normal";

    public void Enter(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} entered Normal state");
    }

    public void Exit(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} exited Normal state");
    }

    public void HandleClick(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} handling click in Normal state");
    }

    public void HandleRender(IUIComponent context, IRenderingContext ctx)
    {
        context?.Render(ctx);
    }
}
