namespace Patterns;

public interface IWidgetFactory
{
    IWidget CreateWidget(WidgetConfig config);
}
