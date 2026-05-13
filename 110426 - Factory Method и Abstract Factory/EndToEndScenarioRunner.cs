using System;
using System.Collections.Generic;

namespace Patterns;

public class EndToEndScenarioRunner
{
	private IThemeFactory _themeFactory;
	private IWidgetFactory _widgetFactory;

	public EndToEndScenarioRunner(IThemeFactory themeFactory, IWidgetFactory widgetFactory)
	{
		_themeFactory = themeFactory;
		_widgetFactory = widgetFactory;
	}

	public void Run()
	{
		Console.WriteLine("End-to-End Сценарий: Интеграция всех паттернов");

		Console.WriteLine("1. Создание диалога через Director + Builder ===");
		DialogBuilder builder = new DialogBuilder(_widgetFactory);
		UI.?

		Console.WriteLine("\n2. Бенчмарк: создание 100 диалогов через Builder ===");
		var builderBenchmark = BenchmarkBuilderCreation(100);

		Console.WriteLine("\n3. Бенчмарк: создание 1 диалога + 99 клонов ===");
		var cloneBenchmark = BenchmarkCloneCreation(99);

		Console.WriteLine("\n4. Итоговые метрики");
		var telemetry = ApplicationTelemetrySingleton.Instance;
		var counts = telemetry.GetOperationCounts();

		Console.WriteLine("Статистика операций:");
		foreach (var kvp in counts)
		{
			Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
		}

		Console.WriteLine($"\nБенчмарк Builder: {builderBenchmark} операций создания фабрик");
		Console.WriteLine($"Бенчмарк Clone: {cloneBenchmark} операций создания фабрик");
		Console.WriteLine($"Экономия: {(1 - (double)cloneBenchmark / builderBenchmark) * 100:F1}%");
	}

	private int BenchmarkBuilderCreation(int count)
	{
		int factoryCallCount = 0;

		for (int i = 0; i < count; i++)
		{
			DialogBuilder builder = new DialogBuilder(_widgetFactory);
			builder.ConfigureTheme(_themeFactory);
			builder.SetTitle("Бенчмарк Диалог " + i);
			builder.AddButton(new ButtonConfig("OK", true));
			builder.AddButton(new ButtonConfig("Cancel"));

			var dialog = builder.Build();

			factoryCallCount += 3;
		}

		return factoryCallCount;
	}

	private int BenchmarkCloneCreation(int cloneCount)
	{
		ClonableDialog original = new ClonableDialog();
		original.Title = "Шаблон диалога";
		original.Buttons.Add(new ClonableButton("Default", "OK"));
		original.Buttons.Add(new ClonableButton("Default", "Cancel"));

		int factoryCallCount = 2;

		for (int i = 0; i < cloneCount; i++)
		{
			var clone = original.Clone();
			clone.Title = "Клон " + i;
			factoryCallCount += 0;
		}

		return factoryCallCount;
	}
}