namespace Patterns;

public class MultiChoiceDialogDirector {
    private IContainerBuilder _builder;
    
    public MultiChoiceDialogDirector(IContainerBuilder builder) {
        _builder = builder;
    }
    
    public IDialog Construct() {
        _builder.SetTitle("Выбор действия");
        _builder.AddButton(new ButtonConfig("Сохранить", true));
        _builder.AddButton(new ButtonConfig("Не сохранять"));
        _builder.AddButton(new ButtonConfig("Отмена"));
        return _builder.Build();
    }
}
