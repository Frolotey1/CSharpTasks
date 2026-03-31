namespace Project;
using System;

public class Circle : Figure {
    public double Radius {get; set;}
    public override double GetArea() {
	return 3.14 * Math.Pow(Radius,2);
    }
    public override double GetPerimeter() {
	return 2 * 3.14 * Radius;
    }
    public static bool operator<(Circle a, Circle b) {
	return a.GetArea() < b.GetArea();
    }
    public static bool operator>(Circle a, Circle b) {
	return a.GetArea() > b.GetArea();
    }
    public static double operator+(Circle a, Circle b) {
	return a.GetPerimeter() + b.GetPerimeter();
    }
    public static bool operator!=(Circle a, Circle b) {
	return a.GetArea() != b.GetArea();
    }
    public static bool operator==(Circle a, Circle b) {
	return a.GetArea() == b.GetArea();
    }
    public double CalculateMaterialCost(double pricePerUnit) {
	return GetArea() * pricePerUnit;
    }
    public override string GetInfo() {
	return string.Format("Имя: {} | Цвет: {} | Радиус: {}",Name,Color,Radius);
    }
}
