using System;
using System.Collections.Generic;
using Patterns;

namespace Patterns;

public class CachedRenderDecorator : UIComponentDecorator {
    private bool _isCacheValid;
    private IApplicationTelemetry _telemetry;

    public CachedRenderDecorator(IUIComponent component, IApplicationTelemetry telemetry) 
        : base(component) {
        _telemetry = telemetry;
        _isCacheValid = false;
    }

    public override void Render(IRenderingContext ctx) {
        if (_isCacheValid) {
            var startTime = DateTime.UtcNow;
            Console.WriteLine($"[CachedRenderDecorator] Использование кэша для {Id}");
            _component.Render(ctx);
            var duration = DateTime.UtcNow - startTime;
            _telemetry.LogOperation("Decorator", "CachedRender", duration, $"ComponentId={Id}");
        }
        else {
            Console.WriteLine($"[CachedRenderDecorator] Первый рендеринг для {Id}, заполняем кэш");
            _component.Render(ctx);
            _isCacheValid = true;
        }
    }

    public override void SetPosition(Point position) {
        _component.SetPosition(position);
        _isCacheValid = false;
        Console.WriteLine($"[CachedRenderDecorator] Кэш инвалидирован для {Id}");
    }
}
