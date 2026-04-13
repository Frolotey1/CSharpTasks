namespace Project;
using System;

public class Bus : PublicTransport {
    private double FuelTankCapacity; 
    private const double FuelConsumption = 20.0;

    public Bus(string number, int capacity, int currentSpeed, int fuelTankCapacity) : base(number, capacity, currentSpeed) {
        FuelTankCapacity = fuelTankCapacity;
    }

    public override string GetInfo() {
        return base.GetInfo() + $"| Вместимость бензобака: {FuelTankCapacity} л";
    }

    public double GetMaxDistance() {
        return (FuelTankCapacity / FuelConsumption) * 25;
    }

    public static void Run() {
        Console.Write("Введите номер автобуса: ");
        string? number = Console.ReadLine();
        Console.Write("Введите вместимость (чел): ");
        int capacity = int.Parse(Console.ReadLine());
        Console.Write("Введите текущую скорость (км/ч): ");
        int speed = int.Parse(Console.ReadLine());
        Console.Write("Введите вместимость бензобака (л): ");
        int fuelTank = int.Parse(Console.ReadLine());

        Bus bus = new Bus(number, capacity, speed, fuelTank);

        Console.WriteLine("Информация об автобусе");
        Console.WriteLine(bus.GetInfo());

        double maxDistance = bus.GetMaxDistance();
        Console.WriteLine($"Максимальное расстояние до опустошения бака: {maxDistance} км");
    }
}
