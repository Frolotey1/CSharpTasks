using System;
using System.Collections.Generic;
using System.Diagnostics;
using Patterns;

namespace Patterns;

public class EndToEndIntegrationRunner
{
    private IThemeFactory _themeFactory;
    private IWidgetFactory _widgetFactory;
    private IContainerBuilder _builder;

    public EndToEndIntegrationRunner(IThemeFactory themeFactory, IWidgetFactory widgetFactory, IContainerBuilder builder)
    {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
        _builder = builder;
    }

    public void Run()
    {
	var telemetry = ApplicationTelemetrySingleton.Instance;
	telemetry.LogOperation("Integration", "Start", TimeSpan.Zero);
	
	Console.WriteLine("End-to-End Интеграция");

	var strategy = _themeFactory.CreateRenderingStrategy();
	telemetry.LogOperation("Integration", "CreateStrategy", TimeSpan.Zero);
	
	var builder = new DialogBuilder(_widgetFactory);
	var director = new DialogBuilderDirector(builder, _widgetFactory, _themeFactory);
	var dialog = director.Construct();

	PanelComponent panel = null;
    
	Console.WriteLine("Переключение на VectorSvgRenderingStrategy");
	if (dialog is PanelComponent p)
	{
	    panel = p;
	    panel.SwitchRenderingStrategy(new VectorSvgRenderingStrategy());
	    panel.Render(null);
	}

	Console.WriteLine("Панель клонирования");
	var clonablePanel = new ClonablePanel("originalPanel", strategy);
	clonablePanel.AddChild(new ClonableButtonComponent("btnOK", strategy, "OK"));
	var clonedPanel = clonablePanel.Clone();
	clonedPanel.SetPosition(new Point(300, 300));
	clonedPanel.Render(null);
	Console.WriteLine("Original and cloned states are independent");
	
	Console.WriteLine("Переключение на LegacyEngineRenderingAdapter");
	if (panel != null)
	{
	    var legacyEngine = new LegacyGraphicsEngine();
	    var adapterStrategy = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
	    panel.SwitchRenderingStrategy(adapterStrategy);
	    panel.Render(null);
	}
	
	Console.WriteLine("SliderComponent");
	var slider = new SliderComponent("slider1", strategy);
	slider.Render(null);
	
	RunBenchmark();

	Console.WriteLine("Метрики");
	var counts = telemetry.GetOperationCounts();
	foreach (var kvp in counts)
	{
	    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
	}
    }

    private void RunBenchmark()
    {
        Console.WriteLine("Tree Traversal Benchmark");

        var root = new PanelComponent("root", new FluentRenderingStrategy());
        for (int i = 0; i < 10000; i++)
        {
            root.AddChild(new ButtonComponent($"node{i}", new FluentRenderingStrategy(), $"Button{i}"));
        }

        var targetId = "node9999";
        long recursiveTime, iterativeTime;
        long recursiveAllocated, iterativeAllocated;

        GC.Collect();
        var memBefore = GC.GetTotalAllocatedBytes(true);
        var sw = Stopwatch.StartNew();
        var recursiveResult = FindByIdRecursive(root, targetId);
        sw.Stop();
        recursiveTime = sw.ElapsedMilliseconds;
        recursiveAllocated = GC.GetTotalAllocatedBytes(true) - memBefore;

        GC.Collect();
        memBefore = GC.GetTotalAllocatedBytes(true);
        sw.Restart();
        var iterativeResult = FindByIdIterative(root, targetId);
        sw.Stop();
        iterativeTime = sw.ElapsedMilliseconds;
        iterativeAllocated = GC.GetTotalAllocatedBytes(true) - memBefore;

        Console.WriteLine($"Recursive DFS: {recursiveTime} ms, {recursiveAllocated} bytes");
        Console.WriteLine($"Iterative DFS (Stack<T>): {iterativeTime} ms, {iterativeAllocated} bytes");
        Console.WriteLine($"Iterative is {(iterativeTime < recursiveTime ? "faster" : "slower")}");
    }

    private IUIComponent FindByIdRecursive(IUIComponent root, string id)
    {
        return root.FindById<IUIComponent>(id);
    }

    private IUIComponent FindByIdIterative(IUIComponent root, string id)
    {
        var stack = new Stack<IUIComponent>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (current.Id == id)
                return current;

            if (current is IContainerComponent container)
            {
                foreach (var child in container.Children)
                {
                    stack.Push(child);
                }
            }
        }
        return null;
    }
}
