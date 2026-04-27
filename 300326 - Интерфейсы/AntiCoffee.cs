namespace Project;
using _300326___Интерфейсы;
using System;

public class StandardClient : ICafeClient, IConsoleOutput {
    public static int HourlyRate { get; set; } = 100;
    public string FullName { get; set; }
    public int Hours { get; set; }

    public StandardClient(string fullName, int hours) {
        FullName = fullName;
        Hours = hours;
    }
    public double TotalCost() {
        return Hours * HourlyRate;
    }
    public void Print() {
        Console.WriteLine("Стандартный клиент");
        Console.WriteLine($"ФИО: {FullName}");
        Console.WriteLine($"Количество часов: {Hours}");
        Console.WriteLine($"Стоимость часа: {HourlyRate} руб.");
        Console.WriteLine($"Итого к оплате: {TotalCost()} руб.");
    }
}

public class VipClient : ICafeClient, IConsoleOutput {
    public static int HourlyRate { get; set; } = 150;
    public string FullName { get; set; }
    public int Hours { get; set; }

    public VipClient(string fullName, int hours) {
        FullName = fullName;
        Hours = hours;
    }
    public double TotalCost() {
        double baseCost = Hours * HourlyRate;
        double tax = baseCost * 0.05;
        return baseCost + tax;
    }
    public void Print() {
        double baseCost = Hours * HourlyRate;
        double tax = baseCost * 0.05;

        Console.WriteLine("VIP клиент");
        Console.WriteLine($"ФИО: {FullName}");
        Console.WriteLine($"Количество часов: {Hours}");
        Console.WriteLine($"Стоимость часа: {HourlyRate} руб.");
        Console.WriteLine($"Налог на роскошь (5%): {tax} руб.");
        Console.WriteLine($"Итого к оплате: {TotalCost()} руб.");
    }
}

public class AntiCoffee {
    public static void Run() {
        Console.WriteLine("1) Стандартный клиент");
        Console.WriteLine("2) VIP клиент");
        Console.Write("Выберите тип клиента: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1) {
            Console.Write("Введите ФИО посетителя: ");
            string name = Console.ReadLine();
            Console.Write("Введите количество часов: ");
            int hours = int.Parse(Console.ReadLine());

            StandardClient client = new StandardClient(name, hours);
            client.Print();
        }
        else if (choice == 2) {
            Console.Write("Введите ФИО посетителя: ");
            string name = Console.ReadLine();
            Console.Write("Введите количество часов: ");
            int hours = int.Parse(Console.ReadLine());

            VipClient client = new VipClient(name, hours);
            client.Print();
        }
        else {
            Console.WriteLine("Неверный выбор!");
        }
    }
}
