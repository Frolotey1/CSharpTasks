using Patterns;
namespace Patterns;

public interface IUISystemFacade {
    IContainerComponent CreateDialog(DialogPreset preset);
    void ApplyGlobalTheme(ThemeType theme);
    void RenderAllToContext(IRenderingContext ctx);
    void LogCurrentMetrics();
    IContainerComponent RootTree { get; }
}
