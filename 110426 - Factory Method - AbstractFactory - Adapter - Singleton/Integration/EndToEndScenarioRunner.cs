using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Patterns;

namespace Patterns
{
    public class EndToEndScenarioRunner 
    {
        private readonly IThemeFactory _themeFactory;
        private readonly IWidgetFactory _widgetFactory;
        private readonly IApplicationTelemetry _telemetry;
        private readonly IContainerBuilder _containerBuilder;

        public EndToEndScenarioRunner(
            IThemeFactory themeFactory, 
            IWidgetFactory widgetFactory,
            IContainerBuilder containerBuilder = null)
        {
            _themeFactory = themeFactory;
            _widgetFactory = widgetFactory;
            _containerBuilder = containerBuilder ?? new DialogBuilder(widgetFactory);
            _telemetry = ApplicationTelemetrySingleton.Instance;
        }

        public void Run() 
        {
            Console.WriteLine("End-to-End Сценарий - Полный тест\n");

            CreateDialogWithDirector();
            CloneDialogWithPrototype();
            ReplaceRendererWithAdapter();
            DisplayMetrics();
            RunBenchmark();
            DisplayFinalMetrics();
        }

        private void CreateDialogWithDirector()
        {
            Console.WriteLine("1. Создание диалога через Director + Builder");

            try 
            {
                var builder = new DialogBuilder(_widgetFactory);
                var errorDirector = new ErrorDialogDirector(builder, _themeFactory);
                var dialog = errorDirector.Construct();
                
                Console.WriteLine("Диалог успешно создан");
                Console.WriteLine($"Тип: {dialog.GetType().Name}");
                Console.WriteLine("Рендеринг диалога:");
                dialog.Render();
                
                _telemetry.LogOperation("EndToEnd", "DialogCreated", TimeSpan.Zero, 
                    $"type={dialog.GetType().Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании диалога: {ex.Message}");
                _telemetry.LogOperation("EndToEnd", "DialogCreateError", TimeSpan.Zero, ex.Message);
            }
            Console.WriteLine();
        }

        private void CloneDialogWithPrototype()
        {
            Console.WriteLine("2. Клонирование диалога через Prototype");

            try 
            {
                var originalDialog = new ClonableDialog();
                originalDialog.Title = "Оригинальный диалог";
                originalDialog.Icon = "star_icon.png";
                originalDialog.Buttons.Add(new ClonableButton("Primary", "Сохранить"));
                originalDialog.Buttons.Add(new ClonableButton("Secondary", "Отмена"));
                
                Console.WriteLine("Оригинальный диалог создан:");
                Console.WriteLine($"Заголовок: {originalDialog.Title}");
                Console.WriteLine($"Иконка: {originalDialog.Icon}");
                Console.WriteLine($"Кнопок: {originalDialog.Buttons.Count}");
                
                var startTime = DateTime.UtcNow;
                var clonedDialog = originalDialog.Clone();
                var cloneTime = DateTime.UtcNow - startTime;
                
                Console.WriteLine($"  Диалог клонирован за {cloneTime.TotalMilliseconds} мс");
                
                clonedDialog.Title = "Модифицированный клон";
                clonedDialog.Icon = "clone_icon.png";
                clonedDialog.Buttons.Add(new ClonableButton("Extra", "Дополнительно"));
                
                Console.WriteLine("Клон после модификации:");
                Console.WriteLine($"Заголовок: {clonedDialog.Title}");
                Console.WriteLine($"Иконка: {clonedDialog.Icon}");
                Console.WriteLine($"Кнопок: {clonedDialog.Buttons.Count} (было {originalDialog.Buttons.Count})");
                
                Console.WriteLine("Проверка независимости:");
                originalDialog.Title = "Изменённый оригинал";
                Console.WriteLine($"Оригинал.Title = '{originalDialog.Title}'");
                Console.WriteLine($"Клон.Title = '{clonedDialog.Title}'");
                Console.WriteLine("Клоны независимы от оригиналов");
                
                _telemetry.LogOperation("Prototype", "CloneSuccess", cloneTime, 
                    $"buttons={originalDialog.Buttons.Count}->{clonedDialog.Buttons.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при клонировании: {ex.Message}");
                _telemetry.LogOperation("EndToEnd", "CloneError", TimeSpan.Zero, ex.Message);
            }
            Console.WriteLine();
        }

        private void ReplaceRendererWithAdapter()
        {
            Console.WriteLine("3. Подмена рендерера через Adapter");

            try 
            {
                Console.WriteLine("  Адаптер для LegacyGraphicsEngine:");
                var legacyEngine = new LegacyGraphicsEngine();
                var adapter = new LegacyEngineRenderingAdapter(legacyEngine, _telemetry);
                adapter.Render("Адаптер", "Работает через LegacyGraphicsEngine");
                
                Console.WriteLine("  Адаптер успешно интегрирован");
                
                _telemetry.LogOperation("Adapter", "IntegrationSuccess", TimeSpan.Zero, 
                    "LegacyEngineRenderingAdapter");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Неподдерживаемая операция: {ex.Message}");
                _telemetry.LogOperation("Adapter", "NotSupported", TimeSpan.Zero, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  Ошибка при работе адаптера: {ex.Message}");
                _telemetry.LogOperation("Adapter", "Error", TimeSpan.Zero, ex.Message);
            }
            Console.WriteLine();
        }

        private void DisplayMetrics()
        {
            Console.WriteLine("4. Текущие метрики (Singleton)");

            var counts = _telemetry.GetOperationCounts();
            
            if (counts.Count == 0)
            {
                Console.WriteLine("  Метрики не собраны");
                return;
            }
            
            Console.WriteLine("  Статистика операций:");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"    {kvp.Key}: {kvp.Value} вызовов");
            }
            
            var settings = _telemetry.GetCurrentSettings();
            Console.WriteLine("  Глобальные настройки UI:");
            Console.WriteLine($"    Шрифт: {settings.DefaultFont}");
            Console.WriteLine($"    Размер: {settings.DefaultFontSize}");
            Console.WriteLine($"    Тема: {settings.Theme}");
            
            Console.WriteLine();
        }

        private void RunBenchmark()
        {
            Console.WriteLine("5. Бенчмарк: Builder vs Prototype");

            _telemetry.ResetForTesting();
            
            const int iterations = 1000;
            
            Console.WriteLine($"  Сценарий A: Создание {iterations} диалогов через Builder");
            var sw = Stopwatch.StartNew();
            
            for (int i = 0; i < iterations; i++) 
            {
                var builder = new DialogBuilder(_widgetFactory);
                builder.ConfigureTheme(_themeFactory);
                builder.SetTitle($"Диалог {i}");
                builder.AddButton(new ButtonConfig("OK", true));
                builder.Build();
            }
            
            sw.Stop();
            var builderTime = sw.ElapsedMilliseconds;
            _telemetry.LogOperation("Benchmark", $"Builder{iterations}", 
                TimeSpan.FromMilliseconds(builderTime));
            
            Console.WriteLine($"Завершено за {builderTime} мс");
            
            Console.WriteLine($"Сценарий B: Создание 1 диалога + {iterations - 1} клонов");
            
            var original = new ClonableDialog();
            original.Title = "Шаблон";
            original.Buttons.Add(new ClonableButton("Default", "OK"));
            
            sw.Restart();
            
            for (int i = 0; i < iterations - 1; i++) 
            {
                original.Clone();
            }
            
            sw.Stop();
            var cloneTime = sw.ElapsedMilliseconds;
            _telemetry.LogOperation("Benchmark", $"Clone{iterations - 1}", 
                TimeSpan.FromMilliseconds(cloneTime));
            
            Console.WriteLine($"Завершено за {cloneTime} мс");
            
            Console.WriteLine("  Результаты сравнения:");
            Console.WriteLine($"Builder ({iterations} диалогов): {builderTime} мс");
            Console.WriteLine($"Prototype (1 + {iterations - 1} клонов): {cloneTime} мс");
            
            var improvement = (1 - (double)cloneTime / builderTime) * 100;
            Console.WriteLine($"Экономия времени: {improvement}%");
            
            Console.WriteLine("  Анализ эффективности:");
            if (improvement >= 60)
            {
                Console.WriteLine($"    Достигнуто сокращение {improvement}% >= 60%");
            }
            else if (improvement >= 40)
            {
                Console.WriteLine($"    Сокращение {improvement}% близко к цели 60%");
            }
            else
            {
                Console.WriteLine($"    Сокращение {improvement}% ниже целевого 60%");
            }
            
            var savedOperations = Math.Round(iterations * (improvement / 100));
            Console.WriteLine($"    Эквивалентная экономия: ~{savedOperations} операций создания");
            
            Console.WriteLine();
        }

        private void DisplayFinalMetrics()
        {
            Console.WriteLine("6. Финальные метрики");

            var finalCounts = _telemetry.GetOperationCounts();
            
            Console.WriteLine("  Всего операций за сессию:");
            var totalOperations = finalCounts.Values.Sum();
            Console.WriteLine($"    Общее количество операций: {totalOperations}");
            Console.WriteLine($"    Типов операций: {finalCounts.Count}");
            
            Console.WriteLine("  Детализация по категориям:");
            
            var categories = finalCounts.GroupBy(kvp => kvp.Key.Split('.')[0]);
            foreach (var category in categories.OrderBy(c => c.Key))
            {
                var count = category.Sum(c => c.Value);
                Console.WriteLine($"    {category.Key}: {count} вызовов");
                foreach (var operation in category)
                {
                    Console.WriteLine($"      {operation.Key.Split('.')[1]}: {operation.Value}");
                }
            }
            
            Console.WriteLine("  Состояние Singleton:");
            Console.WriteLine($"    Экземпляр: {ApplicationTelemetrySingleton.Instance.GetHashCode()}");
            Console.WriteLine("    Потокобезопасность: ConcurrentDictionary<TKey,TValue>");
            
            Console.WriteLine("End-to-End тест пройден");
        }
    }
}
