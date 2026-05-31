using System;
using System.Threading;
using Patterns.Decorator;
using Patterns.Builder;

namespace Patterns;

public class UISystemFacade : IUISystemFacade
{
    private readonly IThemeFactory _themeFactory;
    private readonly IWidgetFactory _widgetFactory;
    private readonly IApplicationTelemetry _telemetry;
    private readonly FlyweightFactory _flyweightFactory;
    private IContainerBuilder _builder;
    private IContainerComponent? _rootTree;
    private readonly ReaderWriterLockSlim _lock = new();

    public IContainerComponent? RootTree => _rootTree;

    public UISystemFacade(IThemeFactory themeFactory, IWidgetFactory widgetFactory, IApplicationTelemetry telemetry)
    {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
        _telemetry = telemetry;
        _flyweightFactory = new FlyweightFactory();
        _builder = new UIContainerBuilder();
        _rootTree = null;
    }

    public IContainerComponent CreateDialog(DialogPreset preset)
    {
        _lock.EnterWriteLock();
        try
        {
            IThemeFactory themeFactory = preset.Theme == ThemeType.Fluent
                ? new FluentThemeFactory()
                : new CupertinoThemeFactory();

            var builder = new DialogBuilder(_widgetFactory);
            builder.ConfigureTheme(themeFactory);
            builder.SetTitle(preset.Title);

            foreach (var buttonText in preset.ButtonTexts)
            {
                builder.AddButton(new ButtonConfig(buttonText, true));
            }

            var director = new DialogBuilderDirector(builder, _widgetFactory, themeFactory);
            var dialog = director.Construct();

            IUIComponent decorated = dialog;

            if (preset.UseBorderDecorator)
            {
                decorated = new BorderDecorator(decorated, Color.Black, 2);
            }
            if (preset.UseLogDecorator)
            {
                decorated = new RenderLogDecorator(decorated, _telemetry);
            }
            if (preset.UseCacheDecorator)
            {
                decorated = new CachedRenderDecorator(decorated, _telemetry);
            }

            if (decorated is IContainerComponent container)
            {
                _rootTree = container;
            }
            else
            {
                _rootTree = dialog;
            }

            return _rootTree;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public IContainerComponent CreateDialogWithProxy(DialogPreset preset)
    {
        _lock.EnterWriteLock();
        try
        {
            var styleKey = new StyleKey("Arial", 12, preset.Theme.ToString());

            var proxy = new VirtualComponentProxy(
                _widgetFactory,
                _flyweightFactory,
                styleKey,
                new Point(100, 100),
                preset.Title,
                "proxy_dialog"
            );

            _rootTree = new PanelComponent("root", _themeFactory.CreateRenderingStrategy());
            _rootTree.AddChild(proxy);

            return _rootTree;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void ApplyGlobalTheme(ThemeType theme)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_rootTree == null) return;

            IRenderingStrategy strategy = theme == ThemeType.Fluent
                ? new FluentRenderingStrategy()
                : new VectorSvgRenderingStrategy();

            ApplyStrategyToTree(_rootTree, strategy);
            ApplyStyleToTree(_rootTree, theme);

            _telemetry.LogOperation("Facade", "ApplyGlobalTheme", TimeSpan.Zero, $"Theme={theme}");
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void ApplyStrategyToTree(IUIComponent component, IRenderingStrategy strategy)
    {
        if (component is UIComponentBase uiBase)
        {
            uiBase.SwitchRenderingStrategy(strategy);
        }

        if (component is VirtualComponentProxy proxy)
        {
            var newStyleKey = new StyleKey("Arial", 12, strategy.GetType().Name);
            proxy.UpdateStyleKey(newStyleKey);
        }

        if (component is IContainerComponent container)
        {
            foreach (var child in container.Children)
            {
                ApplyStrategyToTree(child, strategy);
            }
        }
    }

    private void ApplyStyleToTree(IUIComponent component, ThemeType theme)
    {
        var styleKey = new StyleKey("Arial", 12, theme.ToString());
        var flyweight = _flyweightFactory.GetFlyweight(styleKey);

        if (component is UIComponentBase uiBase)
        {
            uiBase.SetStyle(flyweight);
        }

        if (component is VirtualComponentProxy proxy)
        {
            proxy.UpdateStyleKey(styleKey);
        }

        if (component is IContainerComponent container)
        {
            foreach (var child in container.Children)
            {
                ApplyStyleToTree(child, theme);
            }
        }
    }

    public void RenderAllToContext(IRenderingContext ctx)
    {
        _lock.EnterReadLock();
        try
        {
            if (_rootTree == null) return;

            var startTime = DateTime.UtcNow;
            _rootTree.Render(ctx);
            var duration = DateTime.UtcNow - startTime;
            _telemetry.LogOperation("Facade", "RenderAll", duration);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void LogCurrentMetrics()
    {
        var counts = _telemetry.GetOperationCounts();
        Console.WriteLine("\nТекущие метрики");
        foreach (var kvp in counts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        _flyweightFactory.LogMetrics();
    }

    public FlyweightFactory GetFlyweightFactory() => _flyweightFactory;
}
