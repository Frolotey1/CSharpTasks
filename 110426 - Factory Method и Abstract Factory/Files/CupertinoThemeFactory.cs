namespace Patterns;

public class CupertinoThemeFactory : IThemeFactory
{
    public IButton CreateButton() => new CupertinoButton();
    public ICheckBox CreateCheckBox() => new CupertinoCheckBox();
    public IDialogRenderer CreateDialogRenderer() => new CupertinoDialog();
    public IFontEngine CreateFontEngine() => new CupertinoFont();
    public string ThemeName => "Cupertino (macOS)";
}
