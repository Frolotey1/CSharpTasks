namespace Patterns;

public interface IAcceptable
{
    void Accept(IUIComponentVisitor visitor);
}
