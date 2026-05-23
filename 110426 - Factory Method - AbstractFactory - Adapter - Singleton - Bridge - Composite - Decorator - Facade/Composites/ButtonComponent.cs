using Patterns;

public class ButtonComponent : UIComponentBase, ILeafComponent
{
    public string Text { get; set; }

    public ButtonComponent(string id, IRenderingStrategy strategy, string text) : base(id, strategy)
    {
        Text = text;
        BoundingBox = new Rectangle(0, 0, 100, 40);
    }

    public override void Render(IRenderingContext ctx)
    {
        base.Render(ctx);
        _renderingStrategy.DrawText(Text, new FontMetrics("Arial", 12), new Point(BoundingBox.X + 10, BoundingBox.Y + 15), Color.Black);
    }

    public override T FindById<T>(string id)
    {
        return Id == id ? this as T : null;
    }
}
