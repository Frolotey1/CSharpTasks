namespace Project;
using System;

public class Triangle : Figure, ICostable {
    public double A {get; set;}
    public double B {get; set;}
    public double C {get; set;}
    
    public double halfPerimeter() {
        return (A + B + C) / 2;
    }
    public override double GetArea() {
        double p = halfPerimeter();
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }
    public override double GetPerimeter() {
        return A + B + C;
    }
    public static bool operator<(Triangle a, Triangle b) {
        return a.GetArea() < b.GetArea();
    }
    public static bool operator>(Triangle a, Triangle b) {
        return a.GetArea() > b.GetArea();
    }
    public static double operator+(Triangle a, Triangle b) {
        return a.GetPerimeter() + b.GetPerimeter();
    }
    public static bool operator!=(Triangle a, Triangle b) { 
        return a.GetArea() != b.GetArea();
    }
    public static bool operator==(Triangle a, Triangle b) {
        return a.GetArea() == b.GetArea();
    }
    public double CalculateMaterialCost(double pricePerUnit) {
        return GetArea() * pricePerUnit;
    }
    public override string GetInfo() {
        return string.Format("Имя: {0} | Цвет: {1} | A: {2} | B: {3} | C: {4}", Name, Color, A, B, C);
    }
}
