namespace Patterns;

public class ButtonConfig {
    public string Text { get; set; }
    public bool IsDefault { get; set; }
    
    public ButtonConfig(string text, bool isDefault = false) {
        Text = text;
        IsDefault = isDefault;
    }
}
