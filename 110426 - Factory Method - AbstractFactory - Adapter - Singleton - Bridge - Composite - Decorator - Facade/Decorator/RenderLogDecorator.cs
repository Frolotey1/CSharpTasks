using System;
using Patterns;

namespace Patterns.Decorator;

public class RenderLogDecorator : UIComponentDecorator {
    private IApplicationTelemetry _telemetry;

    public RenderLogDecorator(IUIComponent component, IApplicationTelemetry telemetry) 
        : base(component) {
        _telemetry = telemetry;
    }

    public override void Render(IRenderingContext ctx) {
        var startTime = DateTime.UtcNow;
        
        _component.Render(ctx);
        
        var duration = DateTime.UtcNow - startTime;
        _telemetry.LogOperation("Decorator", "Render", duration, $"ComponentId={Id}");
    }
}
