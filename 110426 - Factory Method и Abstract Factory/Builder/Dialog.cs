using System;
using System.Collections.Generic;

namespace Patterns;

public class Dialog : IDialog
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public List<IButton> Buttons { get; set; }
    public ICheckBox Checkbox { get; set; }
    public List<IWidget> CustomWidgets { get; set; }

    public Dialog()
    {
        Buttons = new List<IButton>();
        CustomWidgets = new List<IWidget>();
    }

    public void Render()
    {
        Console.WriteLine(Title);
        if (!string.IsNullOrEmpty(Icon))
        {
            Console.WriteLine("Иконка: " + Icon);
        }
        
        Console.WriteLine("Кнопки:");
        foreach (IButton button in Buttons)
        {
            button.Render();
        }
        
        if (Checkbox != null)
        {
            Console.Write("Чекбокс: ");
            Checkbox.Render();
        }
        
        if (CustomWidgets.Count > 0)
        {
            Console.WriteLine("Дополнительные виджеты:");
            foreach (IWidget widget in CustomWidgets)
            {
                widget.Render();
            }
        }
    }
}
