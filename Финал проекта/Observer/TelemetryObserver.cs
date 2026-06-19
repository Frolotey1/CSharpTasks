using System;

namespace Patterns.Observer;

public class TelemetryObserver : IUIStateObserver
{
    private readonly IApplicationTelemetry _telemetry;

    public TelemetryObserver(IApplicationTelemetry telemetry)
    {
        _telemetry = telemetry;
    }

    public void OnStateChange(IUIComponent component, UIStateChangeData data)
    {
        _telemetry.LogOperation("Observer.Telemetry", data.StateType, TimeSpan.Zero, 
            $"ComponentId={component?.Id}, Old={data.OldValue}, New={data.NewValue}");
    }

    public void OnError(IUIComponent component, Exception error)
    {
        _telemetry.LogError("Observer.Telemetry", error.Message);
    }
}
