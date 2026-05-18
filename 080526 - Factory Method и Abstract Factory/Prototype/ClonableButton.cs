namespace Patterns;

public class ClonableButton : ButtonComponent, IPrototypical<ClonableButton>
{
    public ClonableButton(string id, IRenderingStrategy strategy, string text) : base(id, strategy, text)
    {
    }

    public new ClonableButton Clone()
    {
        return new ClonableButton(Id + "_clone", _renderingStrategy, Text);
    }
}
