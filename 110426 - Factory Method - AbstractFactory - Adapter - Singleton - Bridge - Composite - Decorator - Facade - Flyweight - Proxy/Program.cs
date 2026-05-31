using Patterns;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("CrossPlatformUISimulator\n");

        var themeFactory = new FluentThemeFactory();
        var widgetFactory = new StandardWidgetFactory();
        var telemetry = ApplicationTelemetrySingleton.Instance;
        var facade = new UISystemFacade(themeFactory, widgetFactory, telemetry);
        var context = new DefaultRenderingContext();

        Console.WriteLine("Flyweight Demo");
        var flyweightFactory = new FlyweightFactory();
        var style1 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 12, "Fluent"));
        var style2 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 12, "Fluent"));
        var style3 = flyweightFactory.GetFlyweight(new StyleKey("Arial", 14, "Fluent"));

        Console.WriteLine($"style1 == style2: {ReferenceEquals(style1, style2)}");
        Console.WriteLine($"style1 == style3: {ReferenceEquals(style1, style3)}");
        flyweightFactory.LogMetrics();

        Console.WriteLine("\nVirtual Proxy Demo");
        var proxy = new VirtualComponentProxy(
            widgetFactory,
            flyweightFactory,
            new StyleKey("Arial", 12, "Fluent"),
            new Point(10, 10),
            "Click Me",
            "btn_proxy"
        );

        Console.WriteLine($"IsMaterialized: {proxy.IsMaterialized}");
        proxy.Render(context);
        Console.WriteLine($"IsMaterialized: {proxy.IsMaterialized}");

        Console.WriteLine("\nProtection Proxy Demo");
        var realButton = new ButtonComponent("real_btn", new FluentRenderingStrategy(), "Protected");
        var protectionProxy = new ProtectionComponentProxy(realButton);

        protectionProxy.SetPosition(new Point(50, 50));
        Console.WriteLine("Position изменён до блокировки");

        protectionProxy.LockComponent();
        try
        {
            protectionProxy.SetPosition(new Point(100, 100));
            Console.WriteLine("ОШИБКА: исключение не выброшено");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"OK: {ex.Message}");
        }

        Console.WriteLine("\nFacade with Proxy Demo");
        var preset = DialogPreset.Default;
        var dialog = facade.CreateDialogWithProxy(preset);
        facade.RenderAllToContext(context);

        Console.WriteLine("\nApplyGlobalTheme to Proxy");
        facade.ApplyGlobalTheme(ThemeType.Cupertino);
        facade.RenderAllToContext(context);

        facade.LogCurrentMetrics();

        Console.WriteLine("\nДемонстрация завершена.");
    }
}
