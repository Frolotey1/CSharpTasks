using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Patterns;

public sealed class ApplicationTelemetrySingleton : IApplicationTelemetry
{
    private static readonly Lazy<ApplicationTelemetrySingleton> _instance = new Lazy<ApplicationTelemetrySingleton>(() => new ApplicationTelemetrySingleton());

    private ConcurrentDictionary<string, int> _operationCounts = new ConcurrentDictionary<string, int>();

    public static ApplicationTelemetrySingleton Instance => _instance.Value;

    private ApplicationTelemetrySingleton() { }

    public void LogOperation(string category, string action, TimeSpan duration, string metadata)
    {
        string key = $"{category}.{action}";
        _operationCounts.AddOrUpdate(key, 1, (_, count) => count + 1);
        Console.WriteLine($"[Telemetry] {category}:{action} - {duration.TotalMilliseconds} ms");
    }

    public void LogError(string category, string error)
    {
        string key = $"{category}.Error";
        _operationCounts.AddOrUpdate(key, 1, (_, count) => count + 1);
        Console.WriteLine($"[Telemetry ERROR] {category}: {error}");
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

    public void LogCurrentMetrics() 
    {
        Console.WriteLine("\nТекущие метрики");
        foreach (var kvp in _operationCounts) 
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
