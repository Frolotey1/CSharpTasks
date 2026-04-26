namespace Project;
using System;
using System.Globalization;

public class Deposit {
    private string FullName {get; set;}
    private double Amount {get; set;}
    private static double InterestRate {get; set;}
    public Deposit(string fullName, double amount, double interestRate) {
        FullName = fullName;
        Amount = amount;
	InterestRate = interestRate;
    }
    public static Deposit operator ++(Deposit d) {
        double percent = d.Amount * (InterestRate / 100);
        d.Amount += percent;
        return d;
    }
    public static double GetInterestRate() {
        return InterestRate;
    }
    public void Show() {
        Console.WriteLine($"Вкладчик: {FullName}, Сумма вклада: {Amount} руб., Процентная ставка: {InterestRate}%");
    }
    public static void Run() {
        Console.Write("Введите ФИО вкладчика: ");
        string? fullName = Console.ReadLine();
        Console.Write("Введите сумму вклада (руб): ");
        double amount = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
	Console.Write("Введите процентную ставку: ");
	double interestRate = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
        Deposit deposit = new Deposit(fullName, amount,interestRate);

        Console.WriteLine("До начисления процентов:");
        deposit.Show();

        deposit++;

        Console.WriteLine("После начисления процентов:");
        deposit.Show();

        Console.WriteLine($"Процентная ставка: {Deposit.GetInterestRate()}%");
    }
}
