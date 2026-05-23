using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Patterns;

public sealed class ApplicationTelemetrySingleton : IApplicationTelemetry {
    private static readonly Lazy<ApplicationTelemetrySingleton> _instance = 
        new Lazy<ApplicationTelemetrySingleton>(() => new ApplicationTelemetrySingleton());

    private readonly ConcurrentDictionary<string, int> _operationCounts = new ConcurrentDictionary<string, int>();
    private GlobalUiSettings _settings;

    public static ApplicationTelemetrySingleton Instance => _instance.Value;

    private ApplicationTelemetrySingleton() {
        _settings = new GlobalUiSettings("Arial", 12, "Light");
    }

    public void LogOperation(string category, string action, TimeSpan duration, string metadata = null) {
        string key = $"{category}.{action}";
        _operationCounts.AddOrUpdate(key, 1, (_, count) => count + 1);
        Console.WriteLine($"[Telemetry] {category}:{action} - {duration.TotalMilliseconds} ms");
        if (metadata != null) {
            Console.WriteLine($"[Telemetry] Metadata: {metadata}");
        }
    }

    public IReadOnlyDictionary<string, int> GetOperationCounts() {
        return new Dictionary<string, int>(_operationCounts);
    }

    public GlobalUiSettings GetCurrentSettings() {
        return new GlobalUiSettings(_settings.DefaultFont, _settings.DefaultFontSize, _settings.Theme);
    }

    public void ResetForTesting() {
        _operationCounts.Clear();
    }
}
