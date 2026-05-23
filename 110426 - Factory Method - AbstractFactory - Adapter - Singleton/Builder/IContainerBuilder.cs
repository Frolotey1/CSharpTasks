namespace Patterns;

public interface IContainerBuilder {
    IContainerBuilder SetTitle(string title);
    IContainerBuilder AddButton(ButtonConfig config);
    IContainerBuilder SetIcon(string icon);
    IContainerBuilder ConfigureTheme(IThemeFactory theme);
    IContainerBuilder AddCustomWidget(IWidget widget);
    IDialog Build();
}
