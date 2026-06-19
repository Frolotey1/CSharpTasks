using System;

namespace Patterns;

public class VirtualComponentProxy : IUIComponent, ILazyComponentProxy
{
    private readonly IWidgetFactory _widgetFactory;
    private readonly FlyweightFactory _flyweightFactory;
    private readonly object _lock = new();

    private StyleKey _styleKey;
    private Point _position;
    private string _text;
    private readonly string _id;
    private bool _enabled = true;
    private bool _visible = true;

    private IUIComponent? _realComponent;
    private IUIStyleFlyweight? _cachedFlyweight;

    public VirtualComponentProxy(
        IWidgetFactory widgetFactory,
        FlyweightFactory flyweightFactory,
        StyleKey styleKey,
        Point position,
        string text,
        string id)
    {
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

    public void Materialize()
    {
        if (_realComponent != null) return;

        lock (_lock)
        {
            if (_realComponent != null) return;

            var strategy = new FluentRenderingStrategy();
            var button = new ButtonComponent(_id, strategy, _text);
            button.SetPosition(_position);
            _realComponent = button;

            if (_realComponent is UIComponentBase uiBase)
            {
                _cachedFlyweight = _flyweightFactory.GetFlyweight(_styleKey);
                uiBase.SetStyle(_cachedFlyweight);
            }

            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.LogOperation("Proxy", "Materialize", TimeSpan.Zero, $"Id={_id}");
        }
    }

    public void UpdateStyleKey(StyleKey newStyleKey)
    {
        _styleKey = newStyleKey;

        if (_realComponent != null && _realComponent is UIComponentBase uiBase)
        {
            var newFlyweight = _flyweightFactory.GetFlyweight(newStyleKey);
            uiBase.SetStyle(newFlyweight);
        }
        else
        {
            _cachedFlyweight = null;
        }
    }

    public void Render(IRenderingContext ctx)
    {
        Materialize();
        _realComponent?.Render(ctx);
    }

    public void SetPosition(Point position)
    {
        _position = position;
        if (_realComponent != null)
            _realComponent.SetPosition(position);
    }

    public T FindById<T>(string id) where T : class, IUIComponent
    {
        if (Id == id)
        {
            Materialize();
            return this as T;
        }

        if (_realComponent != null)
            return _realComponent.FindById<T>(id);

        return null;
    }

    public IUIComponent GetRealSubject() => _realComponent!;

    public Point GetPosition() => _position;
    public string GetText() => _text;
    public StyleKey GetStyleKey() => _styleKey;
    public bool IsEnabled() => _enabled;
    public bool IsVisible() => _visible;
}
