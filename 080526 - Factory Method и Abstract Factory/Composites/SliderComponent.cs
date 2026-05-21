namespace Patterns;

public class SliderComponent : UIComponentBase, ILeafComponent
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int CurrentValue { get; set; }

    public SliderComponent(string id, IRenderingStrategy strategy, int min = 0, int max = 100, int current = 50) : base(id, strategy)
    {
        MinValue = min;
        MaxValue = max;
        CurrentValue = current;
        BoundingBox = new Rectangle(0, 0, 200, 30);
    }

    public override void Render(IRenderingContext ctx)
    {
        _renderingStrategy.DrawBackground(BoundingBox, Color.White);
        _renderingStrategy.DrawBorder(BoundingBox, Color.Black, 1);
        _renderingStrategy.DrawText($"{CurrentValue}", new FontMetrics("Arial", 10), new Point(BoundingBox.X + 90, BoundingBox.Y + 10), Color.Black);
    }

    public override T FindById<T>(string id)
    {
        return Id == id ? this as T : null;
    }
}
