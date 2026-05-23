using System;
using System.Collections.Generic;
namespace Patterns;
using System.Diagnostics;

public class EndToEndScenarioRunner {
    private IThemeFactory _themeFactory;
    private IWidgetFactory _widgetFactory;
    private IApplicationTelemetry _telemetry;

    public EndToEndScenarioRunner(IThemeFactory themeFactory, IWidgetFactory widgetFactory) {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
        _telemetry = ApplicationTelemetrySingleton.Instance;
    }

    public void Run() {
        Console.WriteLine("End-to-End Сценарий\n");

        Console.WriteLine("Создание диалога через Director + Builder");
        var builder = new DialogBuilder(_widgetFactory);
        var errorDirector = new ErrorDialogDirector(builder, _themeFactory);
        var dialog = errorDirector.Construct();
        dialog.Render();

        Console.WriteLine("\nКлонирование диалога");
        var clonableDialog = new ClonableDialog();
        clonableDialog.Title = "Шаблон";
        clonableDialog.Buttons.Add(new ClonableButton("Default", "OK"));
        var clonedDialog = clonableDialog.Clone();
        clonedDialog.Title = "Модифицированный клон";
        clonedDialog.Render();

        Console.WriteLine("\nПодмена рендерера на LegacyGraphicsEngine");
        var legacyEngine = new LegacyGraphicsEngine();
        var adapter = new LegacyEngineRenderingAdapter(legacyEngine, _telemetry);
        adapter.Render("Адаптер", "Работает через LegacyGraphicsEngine");

        Console.WriteLine("\nМетрики Singleton");
        var counts = _telemetry.GetOperationCounts();
        foreach (var kvp in counts) {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        Console.WriteLine("\nБенчмарк: создание 1000 диалогов через Builder");
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++) {
            var b = new DialogBuilder(_widgetFactory);
            b.ConfigureTheme(_themeFactory);
            b.SetTitle($"Диалог {i}");
            b.AddButton(new ButtonConfig("OK", true));
            b.Build();
        }
        sw.Stop();
        _telemetry.LogOperation("Benchmark", "Builder1000", sw.Elapsed);

        Console.WriteLine("\nБенчмарк: создание 1 диалога + 999 клонов");
        var original = new ClonableDialog();
        original.Title = "Оригинал";
        original.Buttons.Add(new ClonableButton("Default", "OK"));
        sw.Restart();
        for (int i = 0; i < 999; i++) {
            original.Clone();
        }
        sw.Stop();
        _telemetry.LogOperation("Benchmark", "Clone999", sw.Elapsed);

        Console.WriteLine("\nИтоговые метрики");
        var finalCounts = _telemetry.GetOperationCounts();
        foreach (var kvp in finalCounts) {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }
    }
}
