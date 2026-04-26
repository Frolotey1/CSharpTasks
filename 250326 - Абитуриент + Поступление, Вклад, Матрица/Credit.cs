namespace Project;
using System;
using System.Globalization;

public class Credit {
    private string _fullName;
    private double _amount;
    private static double _interestRate;
    public string FullName {
        get { return _fullName; }
        set { _fullName = value; }
    }
    public double Amount {
        get { return _amount; }
        set { _amount = value; }
    }
    public static double InterestRate {
        get { return _interestRate; }
        set { _interestRate = value; }
    }
    public Credit(string fullName, double amount) {
        _fullName = fullName;
        _amount = amount;
    }
    public static Credit operator -(Credit c, double payment) {
        c._amount -= payment;
        if (c._amount < 0) c._amount = 0;
        return c;
    }
    public static double GetInterestRate() {
	return _interestRate;
    }
    public void Show() {
        Console.WriteLine($"Заёмщик: {_fullName} | Остаток кредита: {_amount} руб | Процентная ставка: {GetInterestRate()}%");
    }
    public static void Run() {
        Console.Write("Введите ФИО заёмщика: ");
        string fullName = Console.ReadLine();
        Console.Write("Введите сумму кредита в рублях: ");
        double amount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.Write("Введите процентную ставку: ");
        _interestRate = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        Credit credit = new Credit(fullName, amount);

        Console.WriteLine("До платежа:");
        credit.Show();

        Console.Write("Введите сумму платежа: ");
        double payment = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        credit = credit - payment;

        Console.WriteLine("После платежа:");
        credit.Show();

        Console.WriteLine($"Процентная ставка): {GetInterestRate()}%");
    }
}
