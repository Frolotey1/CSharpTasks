using System.Collections.Generic;
using System.Linq;

namespace Patterns;

public class ClonableDialog : IDialog, IPrototypical<ClonableDialog> {
    public string Title { get; set; }
    public string Icon { get; set; }
    public List<IButton> Buttons { get; set; }
    public ICheckBox Checkbox { get; set; }
    public List<IWidget> CustomWidgets { get; set; }

    public ClonableDialog() {
        Buttons = new List<IButton>();
        CustomWidgets = new List<IWidget>();
    }

    public void Render() {
        System.Console.WriteLine(Title);
        if (!string.IsNullOrEmpty(Icon)) {
            System.Console.WriteLine("Иконка: " + Icon);
        }
        System.Console.WriteLine("Кнопки:");
        foreach (var button in Buttons) {
            button.Render();
        }
        if (Checkbox != null) {
            System.Console.Write("Чекбокс: ");
            Checkbox.Render();
        }
        if (CustomWidgets.Count > 0) {
            System.Console.WriteLine("Дополнительные виджеты:");
            foreach (var widget in CustomWidgets) {
                widget.Render();
            }
        }
    }

    public ClonableDialog Clone() {
        ClonableDialog clone = new ClonableDialog();
        clone.Title = this.Title;
        clone.Icon = this.Icon;
        
        foreach (var button in this.Buttons) {
            if (button is IPrototypical<IButton> prototypicalButton) {
                clone.Buttons.Add(prototypicalButton.Clone());
            }
            else {
                clone.Buttons.Add(button);
            }
        }
        
        foreach (var widget in this.CustomWidgets) {
            if (widget is IPrototypical<IWidget> prototypicalWidget) {
                clone.CustomWidgets.Add(prototypicalWidget.Clone());
            }
            else {
                clone.CustomWidgets.Add(widget);
            }
        }
        
        return clone;
    }
}
