using Patterns.Interpreter;

namespace Patterns;

public interface IExpression
{
    void Interpret(UIInterpreterContext context);
}
