namespace Patterns;

public class SetPositionCommand : IUICommand
{
    private readonly IUIComponent _target;
    private readonly int _x, _y;
    private Point _previousPosition;

    public string Description => $"SetPosition({_x},{_y}) to {_target.Id}";

    public SetPositionCommand(IUIComponent target, int x, int y)
    {
        _target = target;
        _x = x;
        _y = y;
        _previousPosition = new Point(target.BoundingBox.X, target.BoundingBox.Y);
    }

    public void Execute()
    {
        _target.SetPosition(new Point(_x, _y));
    }

    public void Undo()
    {
        _target.SetPosition(_previousPosition);
    }
}
