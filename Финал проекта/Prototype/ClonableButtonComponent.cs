using Patterns;

namespace Patterns;

public class ClonableButtonComponent : ButtonComponent, IPrototypical<ClonableButtonComponent>
{
    public ClonableButtonComponent(string id, IRenderingStrategy strategy, string text) 
        : base(id, strategy, text)
    {
    }

    public ClonableButtonComponent Clone()
    {
        return new ClonableButtonComponent(Id + "_clone", _renderingStrategy, Text);
    }
}
