using System;
using Patterns.Interpreter;
using Patterns.Memento;
using Patterns.Observer;
using Patterns.State;
using Patterns.Strategy;
using Patterns.TemplateMethod;
using Patterns.Visitor;

namespace Patterns.Capstone;

public class FullLifecycleCapstoneRunner
{
    private readonly IUISystemFacade _facade;
    private readonly CommandManager _commandManager;
    private readonly UIMementoManager _mementoManager;
    private readonly IApplicationTelemetry _telemetry;
    private readonly Parser _parser;

    public FullLifecycleCapstoneRunner()
    {
        _telemetry = ApplicationTelemetrySingleton.Instance;
        _telemetry.ResetForTesting();
        
        var themeFactory = new FluentThemeFactory();
        var widgetFactory = new StandardWidgetFactory();
        _facade = new UISystemFacade(themeFactory, widgetFactory, _telemetry);
        _commandManager = new CommandManager(_telemetry);
        _mementoManager = new UIMementoManager(20);
        _parser = new Parser();
    }

    public void Run()
    {
        Console.WriteLine("Полный жизненный цикл тестов Capstone\n");

        Console.WriteLine("1. Building UI tree...");
        var preset = DialogPreset.Default with { Title = "Capstone Demo", UseBorderDecorator = true };
        var root = _facade.CreateDialog(preset);
        _telemetry.LogOperation("Capstone", "BuildTree", TimeSpan.Zero, $"RootId={root?.Id}");

        Console.WriteLine("\n2. Сохранение Checkpoint 'инициализация'");
        if (root is IOriginator originator)
        {
            var memento = originator.CreateMemento();
            _mementoManager.SaveCheckpoint("initial", memento);
        }

        Console.WriteLine("\n3. Executing DSL скрипт");
        string script = "SELECT <Button> WHERE Id='dialog_btn_OK_0' -> EXECUTE ApplyTheme('Cupertino') -> SetPosition(50,100)";
        var context = new UIInterpreterContext(_facade, _commandManager, _telemetry);
        var expression = _parser.Parse(script);
        expression.Interpret(context);

        Console.WriteLine("\n4. Демонстрация паттерна State");
        var statefulBtn = new StatefulComponent("demo_btn", new FluentRenderingStrategy(), _telemetry);
        var telemetryObserver = new TelemetryObserver(_telemetry);
        statefulBtn.Attach(telemetryObserver);
        
        Console.WriteLine($"Current state: {statefulBtn.CurrentState.StateName}");
        statefulBtn.TransitionTo(new LoadingState());
        Console.WriteLine($"After Loading: {statefulBtn.CurrentState.StateName}");
        statefulBtn.HandleClick();
        statefulBtn.TransitionTo(new NormalState());

        Console.WriteLine("\n5. Демонстрация стратегии для сетки");
        if (root is PanelComponent panel)
        {
            panel.SetLayoutStrategy(new StackLayoutStrategy(StackDirection.Vertical));
            var layoutContext = new LayoutContext(10, 5, new System.Drawing.Size(400, 300), 1.0f);
            panel.ApplyLayout(layoutContext);
            Console.WriteLine("Applied StackLayoutStrategy");
            
            int[] weights = { 1, 1 };
            panel.SetLayoutStrategy(new GridLayoutStrategy(2, 2, weights, weights));
            panel.ApplyLayout(layoutContext);
            Console.WriteLine("Applied GridLayoutStrategy");
        }

        Console.WriteLine("\n6. Реализация жизненного цикла");
        var lifecycle = new StandardComponentLifecycle(root);
        var uiContext = new UIContext(new DefaultRenderingContext(), _telemetry, root);
        lifecycle.ExecuteLifecycle(uiContext);

        Console.WriteLine("\n7. Реализация Visitor");
        if (_facade is UISystemFacade uiFacade)
        {
            var metricsVisitor = new MetricsCollectorVisitor();
            uiFacade.RunVisitor(metricsVisitor);
            var report = metricsVisitor.GetReport();
            Console.WriteLine($"Metrics: {report.TotalNodes} nodes, {report.ProxyCount} proxies");
            
            var accessibilityVisitor = new AccessibilityTreeVisitor();
            uiFacade.RunVisitor(accessibilityVisitor);
            var tree = accessibilityVisitor.GetTree();
            Console.WriteLine($"Accessibility nodes: {tree.Count}");
        }

        Console.WriteLine("\n8. Сброс Checkpoint");
        var snapshot = _mementoManager.RestoreCheckpoint("initial");
        if (root is IOriginator orig && snapshot is Patterns.Memento.IMemento mem)
            orig.Restore(mem);

        Console.WriteLine("\n9. Undo last command");
        _commandManager.Undo();

        Console.WriteLine("\n10. Финальные метрикиs:");
        _telemetry.LogCurrentMetrics();
        
        Console.WriteLine("\nДемонстрация Capstone тестов закончена");
    }
}
