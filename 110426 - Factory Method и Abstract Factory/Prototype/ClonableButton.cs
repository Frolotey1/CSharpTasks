namespace Patterns;

public class ClonableButton : IButton, IPrototypical<ClonableButton> {
    private string _style;
    private string _text;
    
    public ClonableButton(string style, string text) {
        _style = style;
        _text = text;
    }
    
    public void Render() {
        System.Console.WriteLine(_style + ": " + _text);
    }
    
    public string GetStyle() {
        return _style;
    }
    
    public ClonableButton Clone() {
        return new ClonableButton(_style, _text);
    }
}
