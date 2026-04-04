namespace Project;
using System;

public class Rectangle : Figure, ICostable {
    public double Width {get; set;}
    public double Height {get; set;}
    public override double GetArea() {
	return Width * Height;
    }
    public override double GetPerimeter() {
	return 2 * (Width + Height);
    }
    public static bool operator<(Rectangle a, Rectangle b) {
	return a.GetArea() < b.GetArea();
    }
    public static bool operator>(Rectangle a, Rectangle b) {
	return a.GetArea() > b.GetArea();
    }
    public static double operator+(Rectangle a, Rectangle b) {
	return a.GetPerimeter() + b.GetPerimeter();
    }
    public static bool operator!=(Rectangle a, Rectangle b) {
	return a.GetArea() != b.GetArea();
    }
    public static bool operator==(Rectangle a, Rectangle b) {
	return a.GetArea() == b.GetArea();
    }
    public double CalculateMaterialCost(double pricePerUnit) {
	return GetArea() * pricePerUnit;
    }
    public override string GetInfo() {
	return string.Format("Имя: {} | Цвет: {} | Ширина: {} | Высота: {}",Name,Color,Width,Height);
    }
}
