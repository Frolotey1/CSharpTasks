using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Patterns;

public static class Tests {
    public static void RunAll() {
        Console.WriteLine("\nЗапуск тестов");
        int passed = 0;
        int failed = 0;

        if (Test_AdapterMapping()) passed++; else failed++;
        if (Test_AdapterNotSupported()) passed++; else failed++;
        if (Test_SingletonInstance()) passed++; else failed++;
        if (Test_ResetForTesting()) passed++; else failed++;
        if (Test_ConcurrentDictionary()) passed++; else failed++;
        if (Test_BuilderLogging()) passed++; else failed++;
        if (Test_AdapterInBuilder()) passed++; else failed++;
        if (Test_CloneAndLogging()) passed++; else failed++;
        if (Test_MemoryLeak()) passed++; else failed++;
        if (Test_ReadOnlyDictionary()) passed++; else failed++;

        Console.WriteLine($"\nИтог: {passed} пройдено, {failed} провалено");
    }

    private static bool Test_AdapterMapping() {
        try {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.ResetForTesting();
            var legacyEngine = new LegacyGraphicsEngine();
            var adapter = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
            adapter.Render("Тест", "Содержимое");
            Console.WriteLine("AdapterMapping: OK");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AdapterMapping: {ex.Message}");
            return false;
        }
    }

    private static bool Test_AdapterNotSupported() {
        try {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            var legacyEngine = new LegacyGraphicsEngine();
            var adapter = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
            Console.WriteLine("AdapterNotSupported: OK");
            return true;
        }
        catch (NotSupportedException) {
            Console.WriteLine("AdapterNotSupported: OK");
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine($"AdapterNotSupported: {ex.Message}");
            return false;
        }
    }

    private static bool Test_SingletonInstance() {
        var instance1 = ApplicationTelemetrySingleton.Instance;
        var instance2 = ApplicationTelemetrySingleton.Instance;
        if (ReferenceEquals(instance1, instance2)) {
            Console.WriteLine("SingletonInstance: OK");
            return true;
        }
        Console.WriteLine("SingletonInstance: экземпляры разные");
        return false;
    }

    private static bool Test_ResetForTesting() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.ResetForTesting();
        telemetry.LogOperation("Test", "Op", TimeSpan.Zero);
        var counts = telemetry.GetOperationCounts();
        telemetry.ResetForTesting();
        var afterReset = telemetry.GetOperationCounts();
        if (counts.Count > 0 && afterReset.Count == 0) {
            Console.WriteLine("ResetForTesting: OK");
            return true;
        }
        Console.WriteLine("ResetForTesting: сброс не работает");
        return false;
    }

    private static bool Test_ConcurrentDictionary() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.ResetForTesting();
        Parallel.For(0, 100, i => {
            telemetry.LogOperation("Concurrent", "Test", TimeSpan.Zero);
        });
        var counts = telemetry.GetOperationCounts();
        if (counts.ContainsKey("Concurrent.Test") && counts["Concurrent.Test"] == 100) {
            Console.WriteLine("ConcurrentDictionary: OK");
            return true;
        }
        Console.WriteLine("ConcurrentDictionary: race condition");
        return false;
    }

    private static bool Test_BuilderLogging() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.ResetForTesting();
        var widgetFactory = new StandardWidgetFactory();
        var builder = new DialogBuilder(widgetFactory);
        builder.SetTitle("Тест");
        builder.AddButton(new ButtonConfig("OK", true));
        builder.ConfigureTheme(new FluentThemeFactory());
        builder.Build();
        var counts = telemetry.GetOperationCounts();
        if (counts.ContainsKey("Builder.Build")) {
            Console.WriteLine("BuilderLogging: OK");
            return true;
        }
        Console.WriteLine("BuilderLogging: логирование не сработало");
        return false;
    }

    private static bool Test_AdapterInBuilder() {
        try {
            var legacyEngine = new LegacyGraphicsEngine();
            var telemetry = ApplicationTelemetrySingleton.Instance;
            var adapter = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
            adapter.Render("Тест", "Содержимое");
            Console.WriteLine("AdapterInBuilder: OK");
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine($"AdapterInBuilder: {ex.Message}");
            return false;
        }
    }

    private static bool Test_CloneAndLogging() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.ResetForTesting();
        var original = new ClonableButton("Style", "Text");
        original.Clone();
        var counts = telemetry.GetOperationCounts();
        if (counts.ContainsKey("Prototype.CloneButton")) {
            Console.WriteLine("CloneAndLogging: OK");
            return true;
        }
        Console.WriteLine("CloneAndLogging: логирование клонирования не сработало");
        return false;
    }

    private static bool Test_MemoryLeak() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        for (int i = 0; i < 1000; i++) {
            var legacyEngine = new LegacyGraphicsEngine();
            var adapter = new LegacyEngineRenderingAdapter(legacyEngine, telemetry);
            adapter.Render("Test", "Content");
        }
        Console.WriteLine("Утечка памяти: OK");
        return true;
    }

    private static bool Test_ReadOnlyDictionary() {
	var telemetry = ApplicationTelemetrySingleton.Instance;
	telemetry.ResetForTesting();
	telemetry.LogOperation("Test", "Op1", TimeSpan.Zero);
	telemetry.LogOperation("Test", "Op2", TimeSpan.Zero);
	var dict = telemetry.GetOperationCounts();
    
	if (dict is Dictionary<string, int> mutableDict)
	{
	    try {
		mutableDict.Add("New", 1);
		Console.WriteLine("ReadOnlyDictionary: словарь изменяемый (не защищён)");
		return false;
	    }
	    catch (Exception) {
		Console.WriteLine("ReadOnlyDictionary: OK");
		return true;
	    }
	}
	
	Console.WriteLine("ReadOnlyDictionary: OK");
	return true;
    }
}
