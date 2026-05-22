using Patterns;
using System;

public interface IUIComponent
{
    string Id { get; }
    Rectangle BoundingBox { get; }
    void Render(IRenderingContext ctx);
    void SetPosition(Point position);
    T FindById<T>(string id) where T : class, IUIComponent;
}
