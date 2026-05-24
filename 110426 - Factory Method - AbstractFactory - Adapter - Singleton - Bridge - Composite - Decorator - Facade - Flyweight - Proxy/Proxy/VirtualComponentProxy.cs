using System;
using Patterns;
namespace Patterns;

public class VirtualComponentProxy : IUIComponent, ILazyComponentProxy {
    private readonly IWidgetFactory _widgetFactory;
    private readonly StyleKey _styleKey;
    private readonly Point _position;
    private readonly string _text;
    private readonly string _id;
    private readonly FlyweightFactory _flyweightFactory;
    
    private IUIComponent? _realComponent;
    private readonly object _lock = new();

    public VirtualComponentProxy(IWidgetFactory widgetFactory, FlyweightFactory flyweightFactory, StyleKey styleKey, Point position, string text, string id) {
        _widgetFactory = widgetFactory;
        _flyweightFactory = flyweightFactory;
        _styleKey = styleKey;
        _position = position;
        _text = text;
        _id = id;
    }

    public bool IsMaterialized => _realComponent != null;
    public string Id => _id;
    public Rectangle BoundingBox => _realComponent?.BoundingBox ?? new Rectangle(_position.X, _position.Y, 100, 40);

    public void Materialize() {
        if (_realComponent != null) return;
        
        lock (_lock) {
            if (_realComponent != null) return;
            
            var flyweight = _flyweightFactory.GetFlyweight(_styleKey);
            var strategy = new FluentRenderingStrategy();
            var button = new ButtonComponent(_id, strategy, _text);
            button.SetPosition(_position);
            _realComponent = button;
            
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.LogOperation("Proxy", "Materialize", TimeSpan.Zero, $"Id={_id}");
        }
    }

    public void Render(IRenderingContext ctx) {
        Materialize();
        _realComponent?.Render(ctx);
    }

    public void SetPosition(Point position) {
        if (_realComponent != null)
            _realComponent.SetPosition(position);
    }

    public T FindById<T>(string id) where T : class, IUIComponent {
        if (_realComponent != null)
            return _realComponent.FindById<T>(id);
        
        return Id == id && this is T ? (T)(IUIComponent)this : null;
    }

    public IUIComponent GetRealSubject() => _realComponent!;
}
