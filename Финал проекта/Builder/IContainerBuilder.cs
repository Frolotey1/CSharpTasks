using Patterns;
namespace Patterns;

public interface IContainerBuilder {
    IContainerBuilder SetId(string id);
    IContainerBuilder SetPosition(Point position);
    IContainerBuilder SetRenderingStrategy(IRenderingStrategy strategy);
    IContainerBuilder AddChild(IUIComponent child);
    IContainerBuilder ConfigureTheme(IThemeFactory theme);
    IContainerBuilder SetTitle(string title);
    IContainerBuilder SetIcon(string icon);
    IContainerBuilder AddButton(ButtonConfig config);
    IContainerBuilder ApplyDecorator(IUIComponent component); 
    IContainerComponent Build();
}
