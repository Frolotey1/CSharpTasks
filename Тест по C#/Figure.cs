namespace Project;
using System;

public abstract class Figure {
    public string Color {get; set;}
    public string Name {get; set;}
    public abstract double GetArea();
    public abstract double GetPerimeter();
    public virtual string GetInfo() {
	return string.Format("Имя: {} | Цвет: {}",Name,Color);
    }
}
