namespace Patterns;
using System;

public class ClonableButton : IButton, IPrototypical<ClonableButton> {
    private string _style;
    private string _text;
    
    public ClonableButton(string style, string text) {
        _style = style;
        _text = text;
    }
    
    public void Render() {
        Console.WriteLine(_style + ": " + _text);
    }
    
    public string GetStyle() {
        return _style;
    }
    
    public ClonableButton Clone() {
        var startTime = DateTime.UtcNow;
        var clone = new ClonableButton(_style, _text);
        var duration = DateTime.UtcNow - startTime;
        ApplicationTelemetrySingleton.Instance.LogOperation("Prototype", "CloneButton", duration, $"style={_style}, text={_text}");
        return clone;
    }
}
