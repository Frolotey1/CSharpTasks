namespace Project;
using System;
using System.Globalization;

public class Deposit {
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
    public Deposit(string fullName, double amount) {
        _fullName = fullName;
        _amount = amount;
    }
    public static Deposit operator ++(Deposit d) {
        double percent = d._amount * (_interestRate / 100);
        d._amount += percent;
        return d;
    }
    public static double GetInterestRate()  {
        return _interestRate;
    }
    public void Show() {
        Console.WriteLine($"Вкладчик: {_fullName} | Сумма вклада: {_amount} руб. | Процентная ставка: {GetInterestRate()}%");
    }
    public static void Run() {
        Console.Write("Введите ФИО вкладчика: ");
        string fullName = Console.ReadLine();
        Console.Write("Введите сумму вклада (руб): ");
        double amount = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.Write("Введите процентную ставку: ");
        _interestRate = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        Deposit deposit = new Deposit(fullName, amount);

        Console.WriteLine("До начисления процентов:");
        deposit.Show();

        deposit++;

        Console.WriteLine("После начисления процентов:");
        deposit.Show();

        Console.WriteLine($"Процентная ставка (статический метод): {Deposit.GetInterestRate()}%");
    }
}
