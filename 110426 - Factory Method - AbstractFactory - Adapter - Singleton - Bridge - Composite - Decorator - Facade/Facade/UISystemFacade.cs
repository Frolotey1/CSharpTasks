using System;
using System.Threading;
using Patterns;
using Patterns.Decorator;
using Patterns.Builder;

public class UISystemFacade : IUISystemFacade {
    private readonly IThemeFactory _themeFactory;
    private readonly IWidgetFactory _widgetFactory;
    private readonly IApplicationTelemetry _telemetry;
    private IContainerBuilder _builder;
    private IContainerComponent _rootTree;
    private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public IContainerComponent RootTree => _rootTree;

    public UISystemFacade(IThemeFactory themeFactory, IWidgetFactory widgetFactory, IApplicationTelemetry telemetry) {
        _themeFactory = themeFactory;
        _widgetFactory = widgetFactory;
        _telemetry = telemetry;
        _builder = new UIContainerBuilder();
        _rootTree = null;
    }

    public IContainerComponent CreateDialog(DialogPreset preset) {
	_lock.EnterWriteLock();
	try {
	    IThemeFactory themeFactory = preset.Theme == ThemeType.Fluent 
		? new FluentThemeFactory() 
		: new CupertinoThemeFactory();
	    
	    var builder = new DialogBuilder(_widgetFactory);
	    builder.ConfigureTheme(themeFactory);
	    builder.SetTitle(preset.Title);
        
	    foreach (var text in preset.ButtonTexts)
	    {
		builder.AddButton(new ButtonConfig(text, true));
	    }

	    var director = new DialogBuilderDirector(builder, _widgetFactory, themeFactory);
	    var dialog = director.Construct();
	    
	    IUIComponent decorated = dialog;
	
	    if (preset.UseBorderDecorator) {
		decorated = new BorderDecorator(decorated, Color.Black, 2);
	    }
	    if (preset.UseLogDecorator) {
		decorated = new RenderLogDecorator(decorated, _telemetry);
	    }
	    if (preset.UseCacheDecorator) {
		decorated = new CachedRenderDecorator(decorated, _telemetry);
	    }
	    
	    _rootTree = (IContainerComponent)decorated;
	    return _rootTree;
	}
	finally {
	    _lock.ExitWriteLock();
	}
    }

    public void ApplyGlobalTheme(ThemeType theme) {
        _lock.EnterWriteLock();
        try {
            if (_rootTree == null) return;

            IRenderingStrategy strategy = theme == ThemeType.Fluent 
                ? new FluentRenderingStrategy() 
                : new VectorSvgRenderingStrategy();

            ApplyStrategyToTree(_rootTree, strategy);
            
            _telemetry.LogOperation("Facade", "ApplyGlobalTheme", TimeSpan.Zero, $"Theme={theme}");
        }
        finally {
            _lock.ExitWriteLock();
        }
    }

    private void ApplyStrategyToTree(IUIComponent component, IRenderingStrategy strategy) {
        if (component is UIComponentBase uiBase) {
            uiBase.SwitchRenderingStrategy(strategy);
        }
        
        if (component is IContainerComponent container) {
            foreach (var child in container.Children) {
                ApplyStrategyToTree(child, strategy);
            }
        }
    }

    public void RenderAllToContext(IRenderingContext ctx) {
        _lock.EnterReadLock();
        try {
            if (_rootTree == null) return;
            
            var startTime = DateTime.UtcNow;
            _rootTree.Render(ctx);
            var duration = DateTime.UtcNow - startTime;
            _telemetry.LogOperation("Facade", "RenderAll", duration);
        }
        finally {
            _lock.ExitReadLock();
        }
    }

    public void LogCurrentMetrics() {
        var counts = _telemetry.GetOperationCounts();
        Console.WriteLine("\nТекущие метрики");
        foreach (var kvp in counts) {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
