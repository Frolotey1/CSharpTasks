namespace Project;
using System;

#nullable enable
public class TestFailureException : Exception
{
    public TestFailureException(string message) : base(message) { }
}
