namespace Patterns;
using System;

public class FluentThemeFactory : IThemeFactory {
    public IButton CreateButton() {
        var startTime = DateTime.UtcNow;
        var button = new FluentButton();
        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", "CreateButton", duration, "Fluent");
        return button;
    }

    public ICheckBox CreateCheckBox() {
        var startTime = DateTime.UtcNow;
        var checkbox = new FluentCheckBox();
        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Factory", "CreateCheckBox", duration, "Fluent");
        return checkbox;
    }

    public IDialogRenderer CreateDialogRenderer() {
        return new FluentDialog();
    }

    public IFontEngine CreateFontEngine() {
        return new FluentFont();
    }

    public string ThemeName => "Fluent (Windows 11)";
}
