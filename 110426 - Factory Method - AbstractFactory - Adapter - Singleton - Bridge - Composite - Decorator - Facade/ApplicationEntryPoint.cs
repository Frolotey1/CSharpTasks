using System;
using Patterns;
using Patterns.Decorator;

namespace Patterns
{
    public class ApplicationEntryPoint
    {
        private readonly IUISystemFacade _facade;
        private readonly IApplicationTelemetry _telemetry;
        private readonly IRenderingContext _context;

        public ApplicationEntryPoint()
        {
            var themeFactory = new FluentThemeFactory();
            var widgetFactory = new StandardWidgetFactory();
            _telemetry = ApplicationTelemetrySingleton.Instance;
            _facade = new UISystemFacade(themeFactory, widgetFactory, _telemetry);
            _context = new DefaultRenderingContext();
        }

        public void Run()
        {
            Console.WriteLine("CrossPlatformUISimulator - Демонстрация всех паттернов");

            _telemetry.ResetForTesting();
            CreateDialogWithDecorators();
            CloneDecoratedDialog();
            ApplyGlobalTheme();
            RunDecoratorBenchmark();
            RunCacheBenchmark();
            _facade.LogCurrentMetrics();

            Console.WriteLine("\nДемонстрация завершена.");
        }

        private void CreateDialogWithDecorators()
        {
            Console.WriteLine("1. Создание диалога через Facade с декораторами");

            var preset = new DialogPreset
            {
                Title = "Главное окно приложения",
                Theme = ThemeType.Fluent,
                UseBorderDecorator = true,
                UseLogDecorator = true,
                UseCacheDecorator = true,
                ButtonTexts = new[] { "OK", "Cancel", "Apply" }
            };

            var dialog = _facade.CreateDialog(preset);
            Console.WriteLine("Диалог успешно создан с декораторами:");
            Console.WriteLine($"BorderDecorator: {preset.UseBorderDecorator}");
            Console.WriteLine($"RenderLogDecorator: {preset.UseLogDecorator}");
            Console.WriteLine($"CachedRenderDecorator: {preset.UseCacheDecorator}");

            Console.WriteLine("\nПервый рендеринг (заполнение кэша):");
            _facade.RenderAllToContext(_context);

            Console.WriteLine("\nВторой рендеринг (использование кэша):");
            _facade.RenderAllToContext(_context);
        }

        private void CloneDecoratedDialog()
        {
            Console.WriteLine("\n2. Клонирование декорированного диалога");

            var original = new ClonableDialog();
            original.Title = "Оригинальный диалог";
            original.Buttons.Add(new ClonableButton("Primary", new FluentRenderingStrategy(), "OK"));
            original.Buttons.Add(new ClonableButton("Secondary", new FluentRenderingStrategy(), "Cancel"));

            Console.WriteLine($"  Оригинал: {original.Title}, кнопок: {original.Buttons.Count}");

            if (original is IPrototypical<ClonableDialog> prototypical)
            {
                var cloned = prototypical.Clone();
                cloned.Title = "Модифицированный клон";
                cloned.Buttons.Add(new ClonableButton("Extra", new FluentRenderingStrategy(), "Extra"));

                Console.WriteLine($"Клон: {cloned.Title}, кнопок: {cloned.Buttons.Count}");
                Console.WriteLine("Клоны независимы от оригиналов");
            }
        }

        private void ApplyGlobalTheme()
        {
            Console.WriteLine("\n3. Переключение темы на лету");

            Console.WriteLine("Текущая тема: Fluent");
            _facade.ApplyGlobalTheme(ThemeType.Fluent);
            _facade.RenderAllToContext(_context);

            Console.WriteLine("\nПереключение на Cupertino:");
            _facade.ApplyGlobalTheme(ThemeType.Cupertino);
            _facade.RenderAllToContext(_context);

            Console.WriteLine("\nПереключение обратно на Fluent:");
            _facade.ApplyGlobalTheme(ThemeType.Fluent);
            _facade.RenderAllToContext(_context);

            _telemetry.LogOperation("Demo", "ThemeSwitch", TimeSpan.Zero);
        }

        private void RunDecoratorBenchmark()
        {
            Console.WriteLine("\n4. Бенчмарк накладных расходов декораторов");

            var strategy = new FluentRenderingStrategy();
            var button = new ButtonComponent("btn", strategy, "Test");

            var sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                button.Render(_context);
            }
            sw.Stop();
            var cleanTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"a) Чистый компонент: {cleanTime} мс");

            IUIComponent oneDecorator = new RenderLogDecorator(button, _telemetry);
            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                oneDecorator.Render(_context);
            }
            sw.Stop();
            var oneDecoratorTime = sw.ElapsedMilliseconds;
            var overhead1 = (double)(oneDecoratorTime - cleanTime) / cleanTime * 100;
            Console.WriteLine($"b) 1 декоратор (Log): {oneDecoratorTime} мс (overhead: {overhead1}%)");

            IUIComponent threeDecorators = new BorderDecorator(button, Color.Black, 1);
            threeDecorators = new RenderLogDecorator(threeDecorators, _telemetry);
            threeDecorators = new CachedRenderDecorator(threeDecorators, _telemetry);

            sw.Restart();
            for (int i = 0; i < 1000; i++)
            {
                threeDecorators.Render(_context);
            }
            sw.Stop();
            var threeDecoratorTime = sw.ElapsedMilliseconds;
            var overhead3 = (double)(threeDecoratorTime - cleanTime) / cleanTime * 100;
            Console.WriteLine($"c) 3 декоратора (Border+Log+Cache): {threeDecoratorTime} мс (overhead: {overhead3}%)");

            if (overhead3 <= 60)
                Console.WriteLine("Overhead <60% (ожидаемо для 3 декораторов)");
            else
                Console.WriteLine($"Overhead {overhead3}% превышает ожидаемые 60%");
        }

        private void RunCacheBenchmark()
        {
            Console.WriteLine("\n5. Бенчмарк кэширования CachedRenderDecorator");

            var strategy = new FluentRenderingStrategy();
            var button = new ButtonComponent("btn", strategy, "Test");
            var cachedButton = new CachedRenderDecorator(button, _telemetry);

            var sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 100; i++)
            {
                cachedButton.Render(_context);
            }
            sw.Stop();
            var firstRunTime = sw.ElapsedMilliseconds;

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                cachedButton.Render(_context);
            }
            sw.Stop();
            var cachedRunTime = sw.ElapsedMilliseconds;

            var improvement = (1 - (double)cachedRunTime / firstRunTime) * 100;
            Console.WriteLine($"Первый рендеринг (заполнение кэша): {firstRunTime} мс");
            Console.WriteLine($"Второй рендеринг (из кэша): {cachedRunTime} мс");
            Console.WriteLine($"Экономия времени: {improvement}%");

            if (improvement >= 70)
                Console.WriteLine("CachedRenderDecorator сокращает время повторной отрисовки на >70%");
            else
                Console.WriteLine($"Экономия {improvement}% ниже целевых 70%");

            Console.WriteLine("\nИнвалидация кэша при SetPosition:");
            cachedButton.SetPosition(new Point(100, 100));

            sw.Restart();
            for (int i = 0; i < 100; i++)
            {
                cachedButton.Render(_context);
            }
            sw.Stop();
            var afterMoveTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"После SetPosition (кэш сброшен): {afterMoveTime} мс");
        }
    }
}
