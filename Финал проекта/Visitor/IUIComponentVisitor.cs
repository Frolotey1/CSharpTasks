namespace Patterns;

public interface IUIComponentVisitor
{
    void Visit(ButtonComponent button);
    void Visit(PanelComponent panel);
    void Visit(LabelComponent label);
    void Visit(SliderComponent slider);
    void Visit(VirtualComponentProxy proxy);
    void Visit(ProtectionComponentProxy proxy);
}
