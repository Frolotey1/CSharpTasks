using System;
using System.Collections.Generic;
using Patterns;

public static class SimpleTests
{
    public static void RunAll()
    {
        Console.WriteLine("Запуск тестов");
        
        int passed = 0;
        int failed = 0;
        
        if (TestFindByIdDeep()) passed++; else failed++;
        
        if (TestCyclicReference()) passed++; else failed++;

        if (TestSwitchStrategy()) passed++; else failed++;
        
        if (TestSliderComponent()) passed++; else failed++;
        
        if (TestAdapterMapping()) passed++; else failed++;
        
        if (TestBuilderLogging()) passed++; else failed++;
        
        Console.WriteLine($"Итог: {passed} пройдено, {failed} провалено");
    }
    
    private static bool TestFindByIdDeep()
    {
        try
        {
            var strategy = new FluentRenderingStrategy();
            var root = new PanelComponent("root", strategy);
            var level1 = new PanelComponent("l1", strategy);
            var level2 = new PanelComponent("l2", strategy);
            var target = new ButtonComponent("target", strategy, "Target");
            
            root.AddChild(level1);
            level1.AddChild(level2);
            level2.AddChild(target);
            
            var found = root.FindById<IUIComponent>("target");
            if (found != null && found.Id == "target")
            {
                Console.WriteLine("FindByIdDeep: OK");
                return true;
            }
            Console.WriteLine("FindByIdDeep: элемент не найден");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FindByIdDeep: {ex.Message}");
            return false;
        }
    }
    
    private static bool TestCyclicReference()
    {
        try
        {
            var strategy = new FluentRenderingStrategy();
            var parent = new PanelComponent("parent", strategy);
            var child = new PanelComponent("child", strategy);
            parent.AddChild(child);
            child.AddChild(parent);
            Console.WriteLine("CyclicReference: исключение не выброшено");
            return false;
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("CyclicReference: OK");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CyclicReference: {ex.Message}");
            return false;
        }
    }
    
    private static bool TestSwitchStrategy()
    {
        try
        {
            var strategy = new FluentRenderingStrategy();
            var button = new ButtonComponent("btn", strategy, "Test");
            button.SwitchRenderingStrategy(new VectorSvgRenderingStrategy());
            Console.WriteLine("SwitchStrategy: OK");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SwitchStrategy: {ex.Message}");
            return false;
        }
    }
    
    private static bool TestSliderComponent()
    {
        try
        {
            var strategy = new FluentRenderingStrategy();
            var slider = new SliderComponent("slider", strategy);
            if (slider.CurrentValue == 50)
            {
                Console.WriteLine("SliderComponent: OK");
                return true;
            }
            Console.WriteLine("SliderComponent: значение по умолчанию не 50");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SliderComponent: {ex.Message}");
            return false;
        }
    }
    
    private static bool TestAdapterMapping()
    {
        try
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.ResetForTesting();
            var legacyEngine = new LegacyGraphicsEngine();
            var adapter = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
            adapter.DrawBackground(new Rectangle(0, 0, 100, 100), Color.White);
            adapter.DrawText("Test", new FontMetrics("Arial", 12), new Point(10, 10), Color.Black);
            Console.WriteLine("AdapterMapping: OK");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AdapterMapping: {ex.Message}");
            return false;
        }
    }
    
    private static bool TestBuilderLogging()
    {
        try
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.ResetForTesting();
            var strategy = new FluentRenderingStrategy();
            var builder = new UIContainerBuilder();
            builder.SetId("testPanel");
            builder.SetRenderingStrategy(strategy);
            var panel = builder.Build();
            
            var counts = telemetry.GetOperationCounts();
            if (counts.ContainsKey("Builder.Build"))
            {
                Console.WriteLine("BuilderLogging: OK");
                return true;
            }
            Console.WriteLine("BuilderLogging: логирование не сработало");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"BuilderLogging: {ex.Message}");
            return false;
        }
    }
}

