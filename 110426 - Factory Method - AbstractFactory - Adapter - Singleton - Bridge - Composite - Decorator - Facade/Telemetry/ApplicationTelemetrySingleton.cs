namespace Patterns;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

public sealed class ApplicationTelemetrySingleton : IApplicationTelemetry
{
    private static readonly Lazy<ApplicationTelemetrySingleton> _instance = new Lazy<ApplicationTelemetrySingleton>(() => new ApplicationTelemetrySingleton());

    private ConcurrentDictionary<string, int> _operationCounts = new ConcurrentDictionary<string, int>();

    public static ApplicationTelemetrySingleton Instance => _instance.Value;

    private ApplicationTelemetrySingleton() { }

    public void LogOperation(string category, string action, TimeSpan duration, string metadata = null)
    {
        string key = $"{category}.{action}";
        _operationCounts.AddOrUpdate(key, 1, (_, count) => count + 1);
        Console.WriteLine($"[Telemetry] {category}:{action} - {duration.TotalMilliseconds} ms");
    }

    public IReadOnlyDictionary<string, int> GetOperationCounts()
    {
        return new Dictionary<string, int>(_operationCounts);
    }

    public GlobalUiSettings GetCurrentSettings()
    {
        return new GlobalUiSettings("Arial", 12, "Light");
    }

    public void ResetForTesting()
    {
        _operationCounts.Clear();
    }
}
