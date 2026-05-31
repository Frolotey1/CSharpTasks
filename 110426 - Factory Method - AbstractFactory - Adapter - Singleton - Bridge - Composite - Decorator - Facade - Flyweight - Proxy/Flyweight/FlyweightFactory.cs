using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Patterns;

public class FlyweightFactory
{
    private readonly ConcurrentDictionary<StyleKey, Lazy<IUIStyleFlyweight>> _cache = new();
    private int _hits = 0;
    private int _misses = 0;

    public IUIStyleFlyweight GetFlyweight(StyleKey key)
    {
        var lazyFlyweight = _cache.GetOrAdd(key, new Lazy<IUIStyleFlyweight>(
            () => CreateFlyweight(key),
            LazyThreadSafetyMode.ExecutionAndPublication));

        var flyweight = lazyFlyweight.Value;

        if (_cache.TryGetValue(key, out var existing) && existing == lazyFlyweight)
        {
            Interlocked.Increment(ref _hits);
        }
        else
        {
            Interlocked.Increment(ref _misses);
        }

        return flyweight;
    }

    private IUIStyleFlyweight CreateFlyweight(StyleKey key)
    {
        var palette = key.Theme == "Fluent"
            ? new ColorPalette
            {
                Background = Color.White,
                Foreground = Color.Black,
                Border = Color.Black,
                Hover = Color.Red
            }
            : new ColorPalette
            {
                Background = Color.Black,
                Foreground = Color.White,
                Border = new Color(128, 128, 128),
                Hover = new Color(0, 0, 255)
            };

        var flyweight = new UIStyleFlyweight(
            new FontMetrics(key.FontFamily, key.FontSize),
            palette,
            null
        );

        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Flyweight", "Create", TimeSpan.Zero,
            $"Key={key.FontFamily},{key.FontSize},{key.Theme},Id={flyweight.StyleId}");

        return flyweight;
    }

    public void LogMetrics()
    {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Flyweight", "Metrics", TimeSpan.Zero,
            $"Hits={_hits}, Misses={_misses}, Total={_cache.Count}");
    }

    public int GetTotalInstancesCount() => _cache.Count;
    public int GetHits() => _hits;
    public int GetMisses() => _misses;
}
