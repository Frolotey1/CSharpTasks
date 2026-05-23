namespace Patterns;
using System;

public class CupertinoThemeFactory : IThemeFactory {
    public IButton CreateButton() {
        var startTime = DateTime.UtcNow;
        var button = new CupertinoButton();
        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", "CreateButton", duration, "Cupertino");
        return button;
    }

    public ICheckBox CreateCheckBox() {
        var startTime = DateTime.UtcNow;
        var checkbox = new CupertinoCheckBox();
        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", "CreateCheckBox", duration, "Cupertino");
        return checkbox;
    }

    public IDialogRenderer CreateDialogRenderer() {
        return new CupertinoDialog();
    }

    public IFontEngine CreateFontEngine() {
        return new CupertinoFont();
    }

    public string ThemeName => "Cupertino (macOS)";
}
