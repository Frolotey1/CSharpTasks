using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Patterns.Interpreter;

public class Parser
{
    private string _script;
    private int _position;
    private List<string> _tokens;
    private int _tokenIndex;

    public Patterns.IExpression Parse(string script)
    {
        _script = script;
        _position = 0;
        _tokens = Tokenize(script);
        _tokenIndex = 0;
        
        var expr = ParseStatement();
        
        if (_tokenIndex < _tokens.Count)
            throw new ParseException("Unexpected tokens after end of statement", _position, null);
        
        return expr;
    }

    private List<string> Tokenize(string script)
    {
        var tokens = new List<string>();
        var regex = new Regex(@"(\bSELECT\b|\bWHERE\b|\bEXECUTE\b|\b->\b|<[^>]+>|\w+|[=!<>]+|'[^']*'|\([^)]*\))", RegexOptions.IgnoreCase);
        var matches = regex.Matches(script);
        
        foreach (Match match in matches)
        {
            tokens.Add(match.Value);
        }
        return tokens;
    }

    private string Peek()
    {
        return _tokenIndex < _tokens.Count ? _tokens[_tokenIndex] : null;
    }

    private string Consume()
    {
        if (_tokenIndex >= _tokens.Count)
            throw new ParseException("Unexpected end of input", _position, null);
        return _tokens[_tokenIndex++];
    }

    private void Expect(string expected)
    {
        var actual = Peek();
        if (actual == null || !actual.Equals(expected, StringComparison.OrdinalIgnoreCase))
            throw new ParseException($"Expected '{expected}', got '{actual}'", _position, expected);
        Consume();
    }

    private Patterns.IExpression ParseStatement()
    {
        var token = Peek()?.ToUpperInvariant();
        
        return token switch
        {
            "SELECT" => ParseQuery(),
            "EXECUTE" => ParseCommandChain(),
            _ => throw new ParseException($"Expected SELECT or EXECUTE, got '{token}'", _position, null)
        };
    }

    private Patterns.IExpression ParseQuery()
    {
        Expect("SELECT");
        var typeSelector = ParseTypeSelector();
        
        PredicateExpression predicate = null;
        if (Peek()?.ToUpperInvariant() == "WHERE")
        {
            Consume();
            predicate = ParsePredicate();
        }
        
        var selectExpr = new SelectExpression(typeSelector, predicate);
        
        if (Peek()?.ToUpperInvariant() == "->" || Peek()?.ToUpperInvariant() == "EXECUTE")
        {
            var commandChain = ParseCommandChain();
            return new ChainExpression(new Patterns.IExpression[] { selectExpr, commandChain });
        }
        
        return selectExpr;
    }

    private TypeSelectorExpression ParseTypeSelector()
    {
        var token = Consume();
        if (!token.StartsWith("<") || !token.EndsWith(">"))
            throw new ParseException($"Invalid type selector, expected <Type> got '{token}'", _position, null);
        
        var typeName = token.Substring(1, token.Length - 2);
        return new TypeSelectorExpression(typeName);
    }

    private PredicateExpression ParsePredicate()
    {
        var property = Consume();
        var op = Consume();
        var value = Consume();
        
        if (value.StartsWith("'") && value.EndsWith("'"))
            value = value.Substring(1, value.Length - 2);
        
        return new PredicateExpression(property, op, value);
    }

    private Patterns.IExpression ParseCommandChain()
    {
        Expect("EXECUTE");
        var actions = new List<Patterns.IExpression>();
        
        actions.Add(ParseAction());
        
        while (Peek() == "->")
        {
            Consume();
            actions.Add(ParseAction());
        }
        
        return new ChainExpression(actions.ToArray());
    }

    private ActionExpression ParseAction()
    {
        var actionName = Consume();
        
        string[] arguments = Array.Empty<string>();
        if (Peek() == "(")
        {
            Consume();
            var argsToken = Consume();
            if (argsToken != ")")
            {
                arguments = argsToken.Split(',').Select(a => a.Trim().Trim('\'')).ToArray();
                Expect(")");
            }
        }
        
        return new ActionExpression(actionName, arguments);
    }
}
