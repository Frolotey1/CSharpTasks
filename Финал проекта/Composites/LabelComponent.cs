using Patterns;

public class LabelComponent : UIComponentBase, ILeafComponent
{
    public string Text { get; set; }

    public LabelComponent(string id, IRenderingStrategy strategy, string text) : base(id, strategy)
    {
        Text = text;
        BoundingBox = new Rectangle(0, 0, 100, 30);
    }

    public override void Render(IRenderingContext ctx)
    {
        _renderingStrategy.DrawText(Text, new FontMetrics("Arial", 12), new Point(BoundingBox.X, BoundingBox.Y), Color.Black);
    }

    public override T FindById<T>(string id)
    {
        return Id == id ? this as T : null;
    }
}
