using System;

namespace Patterns;

public class ProtectionComponentProxy : IUIComponent, ILazyComponentProxy
{
    private readonly IUIComponent _realComponent;
    private bool _isLocked = false;

    public ProtectionComponentProxy(IUIComponent realComponent)
    {
        _realComponent = realComponent;
    }

    public bool IsMaterialized => true;
    public string Id => _realComponent.Id;
    public Rectangle BoundingBox => _realComponent.BoundingBox;

    public void LockComponent()
    {
        _isLocked = true;
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Proxy", "Lock", TimeSpan.Zero, $"Id={Id}");
    }

    public void Materialize() { }

    public IUIComponent GetRealSubject() => _realComponent;

    public void Render(IRenderingContext ctx)
    {
        _realComponent.Render(ctx);
    }

    public void SetPosition(Point position)
    {
        if (_isLocked)
            throw new InvalidOperationException($"Компонент {Id} заблокирован, изменение позиции запрещено");
        _realComponent.SetPosition(position);
    }

    public void SetText(string text)
    {
        if (_isLocked)
            throw new InvalidOperationException($"Компонент {Id} заблокирован, изменение текста запрещено");

        if (_realComponent is ButtonComponent button)
            button.Text = text;
        else if (_realComponent is LabelComponent label)
            label.Text = text;
        else if (_realComponent is SliderComponent slider)
            slider.CurrentValue = int.TryParse(text, out var val) ? val : slider.CurrentValue;
    }

    public void AddChild(IUIComponent child)
    {
        if (_isLocked)
            throw new InvalidOperationException($"Компонент {Id} заблокирован, добавление детей запрещено");

        if (_realComponent is IContainerComponent container)
            container.AddChild(child);
        else
            throw new NotSupportedException($"Компонент {Id} не поддерживает добавление дочерних элементов");
    }

    public void RemoveChild(IUIComponent child)
    {
        if (_isLocked)
            throw new InvalidOperationException($"Компонент {Id} заблокирован, удаление детей запрещено");

        if (_realComponent is IContainerComponent container)
            container.RemoveChild(child);
        else
            throw new NotSupportedException($"Компонент {Id} не поддерживает удаление дочерних элементов");
    }

    public T FindById<T>(string id) where T : class, IUIComponent
    {
        return _realComponent.FindById<T>(id);
    }
}
