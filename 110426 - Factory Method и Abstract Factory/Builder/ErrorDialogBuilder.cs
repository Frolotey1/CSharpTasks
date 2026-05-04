namespace Patterns;

public class ErrorDialogDirector {
    private IContainerBuilder _builder;
    
    public ErrorDialogDirector(IContainerBuilder builder) {
        _builder = builder;
    }
    
    public IDialog Construct() {
        _builder.SetTitle("Ошибка");
        _builder.SetIcon("error.png");
        _builder.AddButton(new ButtonConfig("OK", true));
        return _builder.Build();
    }
}
