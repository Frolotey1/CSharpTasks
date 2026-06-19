using System;
using System.Threading;
using Patterns.Observer;

namespace Patterns.State;

public class StatefulComponent : UIComponentBase, ISubject
{
    private IComponentState _currentState;
    private readonly ReaderWriterLockSlim _stateLock = new();
    private readonly SafeObserverSubject _observerSubject;

    public IComponentState CurrentState => _currentState;

    public StatefulComponent(string id, IRenderingStrategy strategy, IApplicationTelemetry telemetry) 
        : base(id, strategy)
    {
        _currentState = new NormalState();
        _observerSubject = new SafeObserverSubject(telemetry);
        _currentState.Enter(this);
    }

    public void TransitionTo(IComponentState newState)
    {
        if (newState == null) throw new ArgumentNullException(nameof(newState));
        
        _stateLock.EnterWriteLock();
        try
        {
            string oldStateName = _currentState.StateName;
            _currentState.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
            
            var changeData = new UIStateChangeData("State", oldStateName, newState.StateName, DateTime.UtcNow);
            _observerSubject.Notify(changeData);
        }
        finally
        {
            _stateLock.ExitWriteLock();
        }
    }

    public override void Render(IRenderingContext ctx)
    {
        _stateLock.EnterReadLock();
        try
        {
            _currentState.HandleRender(this, ctx);
        }
        finally
        {
            _stateLock.ExitReadLock();
        }
    }

    public void HandleClick()
    {
        _stateLock.EnterReadLock();
        try
        {
            _currentState.HandleClick(this);
        }
        finally
        {
            _stateLock.ExitReadLock();
        }
    }

    public override T FindById<T>(string id)
    {
        return Id == id ? this as T : null;
    }

    public void Attach(IUIStateObserver observer) => _observerSubject.Attach(observer);
    public void Detach(IUIStateObserver observer) => _observerSubject.Detach(observer);
    public void Notify(UIStateChangeData data) => _observerSubject.Notify(data);
}
