using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Patterns.Observer;

public class SafeObserverSubject : ISubject, IDisposable
{
    private List<IUIStateObserver> _observers = new();
    private readonly ReaderWriterLockSlim _lock = new();
    private readonly IApplicationTelemetry _telemetry;
    private bool _disposed;

    public SafeObserverSubject(IApplicationTelemetry telemetry)
    {
        _telemetry = telemetry;
    }

    public void Attach(IUIStateObserver observer)
    {
        if (observer == null) throw new ArgumentNullException(nameof(observer));
        
        _lock.EnterWriteLock();
        try
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Detach(IUIStateObserver observer)
    {
        if (observer == null) return;
        
        _lock.EnterWriteLock();
        try
        {
            _observers.Remove(observer);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Notify(UIStateChangeData data)
    {
        if (_disposed) return;
        
        List<IUIStateObserver> snapshot;
        _lock.EnterReadLock();
        try
        {
            snapshot = _observers.ToList();
        }
        finally
        {
            _lock.ExitReadLock();
        }
        
        foreach (var observer in snapshot)
        {
            try
            {
                observer.OnStateChange(null, data);
            }
            catch (Exception ex)
            {
                _telemetry.LogError("Observer", $"Notify failed: {ex.Message}");
                observer.OnError(null, ex);
            }
        }
        
        _telemetry.LogOperation("Observer", "Notify", TimeSpan.Zero, $"StateType={data.StateType}");
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _lock.EnterWriteLock();
        try
        {
            _observers.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
            _lock.Dispose();
        }
    }
}
