using Patterns;
using Patterns.Iterators;

namespace Patterns.Interpreter;

public class UIInterpreterContext
{
    public IUISystemFacade Facade { get; }
    public CommandManager CommandManager { get; }
    public IApplicationTelemetry Telemetry { get; }
    public IteratorFactory IteratorFactory { get; }

    public UIInterpreterContext(IUISystemFacade facade, CommandManager commandManager, IApplicationTelemetry telemetry)
    {
        Facade = facade;
        CommandManager = commandManager;
        Telemetry = telemetry;
        IteratorFactory = new IteratorFactory();
    }
}
