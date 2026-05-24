using System.Collections.Concurrent;
using System;

namespace Patterns;

public class FlyweightFactory {
    private readonly ConcurrentDictionary<StyleKey, IUIStyleFlyweight> _cache = new();
    private int _hits = 0;
    private int _misses = 0;

    public IUIStyleFlyweight GetFlyweight(StyleKey key) {
        if (_cache.TryGetValue(key, out var flyweight)) {
            System.Threading.Interlocked.Increment(ref _hits);
            return flyweight;
        }

        System.Threading.Interlocked.Increment(ref _misses);
        
        var newFlyweight = CreateFlyweight(key);
        _cache[key] = newFlyweight;
        
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Flyweight", "Create", TimeSpan.Zero, $"Key={key.FontFamily},{key.FontSize},{key.Theme}");
        
        return newFlyweight;
    }

    private IUIStyleFlyweight CreateFlyweight(StyleKey key) {
        var palette = key.Theme == "Fluent" 
            ? new ColorPalette { Background = Color.White, Foreground = Color.Black, Border = Color.Black, Hover = Color.Red }
            : new ColorPalette { Background = Color.Black, Foreground = Color.White, Border = new Color(128, 128, 128), Hover = new Color(0, 0, 255) };
        
        return new UIStyleFlyweight(
            new FontMetrics(key.FontFamily, key.FontSize),
            palette,
            null
        );
    }

    public void LogMetrics() {
        var telemetry = ApplicationTelemetrySingleton.Instance;
        telemetry.LogOperation("Flyweight", "Metrics", TimeSpan.Zero, $"Hits={_hits}, Misses={_misses}, Total={_cache.Count}");
    }
}
