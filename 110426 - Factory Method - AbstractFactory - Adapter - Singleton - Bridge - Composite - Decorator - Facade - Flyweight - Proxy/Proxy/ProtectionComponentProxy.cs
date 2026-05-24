using System;
namespace Patterns.Proxy;

public class ProtectionComponentProxy : IUIComponent, ILazyComponentProxy {
    private readonly IUIComponent _realComponent;
    private bool _isLocked = false;

    public ProtectionComponentProxy(IUIComponent realComponent) {
        _realComponent = realComponent;
    }

    public bool IsMaterialized => true;
    public string Id => _realComponent.Id;
    public Rectangle BoundingBox => _realComponent.BoundingBox;

    public void LockComponent() {
        _isLocked = true;
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Proxy", "Lock", TimeSpan.Zero, $"Id={Id}");
    }

    public void Materialize() {}

    public IUIComponent GetRealSubject() => _realComponent;

    public void Render(IRenderingContext ctx) {
        _realComponent.Render(ctx);
    }

    public void SetPosition(Point position) {
        if (_isLocked)
            throw new InvalidOperationException($"Компонент {Id} заблокирован, изменение позиции запрещено");
        _realComponent.SetPosition(position);
    }

    public T FindById<T>(string id) where T : class, IUIComponent {
        return _realComponent.FindById<T>(id);
    }
}
