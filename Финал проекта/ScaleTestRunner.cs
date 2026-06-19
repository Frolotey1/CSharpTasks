using System;
using Patterns;

namespace Patterns.ScaleTest;

public class ScaleTestRunner {
    public static void Run() {
        Console.WriteLine("\nМасштабный тест: Flyweight + Proxy\n");
        
        var telemetry = ApplicationTelemetrySingleton.Instance;
        var flyweightFactory = new FlyweightFactory();
        var widgetFactory = new StandardWidgetFactory();
        
        var style1 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 12, "Fluent"));
        var style2 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 14, "Fluent"));
        var style3 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 12, "Cupertino"));
        
        Console.WriteLine("1. Создание 5000 прокси (виртуальных)");
        var startTime = DateTime.UtcNow;
        
        var proxies = new VirtualComponentProxy[5000];
        for (int i = 0; i < 5000; i++) {
            var styleKey = i % 3 == 0 ? new StyleKey("Arial", 12, "Fluent")
                        : i % 3 == 1 ? new StyleKey("Arial", 14, "Fluent")
                        : new StyleKey("Arial", 12, "Cupertino");
            
            proxies[i] = new VirtualComponentProxy(widgetFactory, flyweightFactory, styleKey, new Point(10, 10 + i * 30), $"Button{i}", $"btn{i}");
        }
        
        var buildTime = DateTime.UtcNow - startTime;
        telemetry.LogOperation("ScaleTest", "BuildProxies", buildTime, "5000 proxies");
        
        Console.WriteLine("\n2. Материализация ~1500 прокси");
        startTime = DateTime.UtcNow;
        for (int i = 0; i < 1500; i++) {
            proxies[i].Materialize();
        }
        var materializeTime = DateTime.UtcNow - startTime;
        telemetry.LogOperation("ScaleTest", "Materialize", materializeTime, "1500 proxies");
        
        Console.WriteLine("\n3. Статистика Flyweight");
        flyweightFactory.LogMetrics();
        
        var style1b = flyweightFactory.GetFlyweight(new StyleKey("Arial", 12, "Fluent"));
        Console.WriteLine($"\n4. Проверка Flyweight: ReferenceEquals(style1, style1b) = {ReferenceEquals(style1, style1b)}");
        
        Console.WriteLine("\n5. Тест ProtectionProxy (пропущен, требуется ProtectionComponentProxy)");
        
        Console.WriteLine("\n6. Итоговые метрики");
        telemetry.LogCurrentMetrics();
        
        Console.WriteLine("\nМасштабный тест завершён");
    }
}
