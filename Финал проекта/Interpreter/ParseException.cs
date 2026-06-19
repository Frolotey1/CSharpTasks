using System;

namespace Patterns.Interpreter;

public class ParseException : Exception
{
    public int Position { get; }
    public string ExpectedToken { get; }

    public ParseException(string message, int position, string expectedToken) 
        : base(message)
    {
        Position = position;
        ExpectedToken = expectedToken;
    }
}
