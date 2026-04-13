namespace Project;
using System;

public class Trolleybus : PublicTransport {
    private double BatteryCapacity; 
    private const double PowerConsumption = 200.0;

    public Trolleybus(string number, int capacity, int currentSpeed, int batteryCapacity) : base(number, capacity, currentSpeed) {
        BatteryCapacity = batteryCapacity;
    }

    public override string GetInfo() {
        return base.GetInfo() + $"| Ёмкость аккумулятора: {BatteryCapacity} кВт·ч";
    }

    public double GetMaxDistance() {
        return (BatteryCapacity / PowerConsumption) * 70;
    }

    public static void Run() {
        Console.WriteLine("Троллейбус");

        Console.Write("Введите номер троллейбуса: ");
        string number = Console.ReadLine();
        Console.Write("Введите вместимость (чел): ");
        int capacity = int.Parse(Console.ReadLine());
        Console.Write("Введите текущую скорость (км/ч): ");
        int speed = int.Parse(Console.ReadLine());
        Console.Write("Введите ёмкость аккумулятора (кВт·ч): ");
        int battery = int.Parse(Console.ReadLine());

        Trolleybus trolleybus = new Trolleybus(number, capacity, speed, battery);

        Console.WriteLine("Информация о троллейбусе");
        Console.WriteLine(trolleybus.GetInfo());

        double maxDistance = trolleybus.GetMaxDistance();
        Console.WriteLine($"Максимальное расстояние до разряда аккумулятора: {maxDistance} км");
    }
}
