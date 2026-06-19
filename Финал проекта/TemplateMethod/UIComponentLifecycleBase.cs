using System;

namespace Patterns.TemplateMethod;

public abstract class UIComponentLifecycleBase
{
    public void ExecuteLifecycle(UIContext ctx)
    {
        Initialize(ctx);
        LoadResources(ctx);
        ApplyTheme(ctx);
        
        if (!Validate(ctx))
        {
            OnValidationFailed(ctx);
            return;
        }
        
        PreRender(ctx);
        Render(ctx);
        PostRender(ctx);
        Cleanup(ctx);
    }

    protected abstract void Initialize(UIContext ctx);
    protected virtual void LoadResources(UIContext ctx) { }
    protected virtual void ApplyTheme(UIContext ctx) { }
    protected abstract bool Validate(UIContext ctx);
    protected virtual void OnValidationFailed(UIContext ctx) { }
    protected virtual void PreRender(UIContext ctx) { }
    protected abstract void Render(UIContext ctx);
    protected virtual void PostRender(UIContext ctx) { }
    protected virtual void Cleanup(UIContext ctx) { }
}
