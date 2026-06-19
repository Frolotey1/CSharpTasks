using System;

namespace Patterns.Observer;

public interface IUIStateObserver
{
    void OnStateChange(IUIComponent component, UIStateChangeData data);
    void OnError(IUIComponent component, Exception error);
}
