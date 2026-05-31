namespace Patterns;

public interface IUISystemFacade
{
    IContainerComponent CreateDialog(DialogPreset preset);
    IContainerComponent CreateDialogWithProxy(DialogPreset preset);
    void ApplyGlobalTheme(ThemeType theme);
    void RenderAllToContext(IRenderingContext ctx);
    void LogCurrentMetrics();
    IContainerComponent? RootTree { get; }
}
