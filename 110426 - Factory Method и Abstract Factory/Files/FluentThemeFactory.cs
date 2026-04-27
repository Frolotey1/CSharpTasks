namespace Patterns;

public class FluentThemeFactory : IThemeFactory
{
    public IButton CreateButton() => new FluentButton();
    public ICheckBox CreateCheckBox() => new FluentCheckBox();
    public IDialogRenderer CreateDialogRenderer() => new FluentDialog();
    public IFontEngine CreateFontEngine() => new FluentFont();
    public string ThemeName => "Fluent (Windows 11)";
}
