namespace Patterns;

public class ClonableWidget : IWidget, IPrototypical<ClonableWidget> {
    private string _type;
    private string _name;
    
    public ClonableWidget(string type, string name) {
        _type = type;
        _name = name;
    }
    public void Render() {
        System.Console.WriteLine("  [Виджет] " + _type + ": " + _name);
    }
    
    public ClonableWidget Clone() {
        return new ClonableWidget(_type, _name);
    }
}
