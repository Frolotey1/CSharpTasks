using System.Collections.Generic;

namespace Patterns;

public interface IDialog {
    string Title { get; }
    string Icon { get; }
    List<IButton> Buttons { get; }
    ICheckBox Checkbox { get; set; }
    List<IWidget> CustomWidgets { get; }
    void Render();
}
