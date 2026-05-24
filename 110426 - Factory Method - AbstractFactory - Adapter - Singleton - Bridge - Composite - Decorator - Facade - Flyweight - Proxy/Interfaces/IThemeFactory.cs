namespace Patterns;

public interface IThemeFactory
{
    IButton CreateButton();
    ICheckBox CreateCheckBox();
    IDialogRenderer CreateDialogRenderer();
    IFontEngine CreateFontEngine();
    IRenderingStrategy CreateRenderingStrategy();
    string ThemeName { get; }
}
