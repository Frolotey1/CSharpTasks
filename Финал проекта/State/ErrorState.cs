using System;

namespace Patterns.State;

public class ErrorState : IComponentState
{
    public string StateName => "Error";

    public void Enter(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} entered Error state - visual error mode");
    }

    public void Exit(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} exited Error state");
    }

    public void HandleClick(IUIComponent context)
    {
        Console.WriteLine($"[State] {context?.Id} click handled as error recovery attempt");
    }

    public void HandleRender(IUIComponent context, IRenderingContext ctx)
    {
        Console.WriteLine($"[State] {context?.Id} rendering error visual style");
    }
}
