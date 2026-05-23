namespace Patterns;
using System.Collections.Generic;

public interface IDialog {
    string Title { get; }
    string Icon { get; }
    List<IButton> Buttons { get; }
    ICheckBox Checkbox { get; set; }
    List<IWidget> CustomWidgets { get; }
    void Render();
}
