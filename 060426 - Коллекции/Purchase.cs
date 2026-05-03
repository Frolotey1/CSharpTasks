namespace Project;
using System;

public class Purchase<T> where T : IPayment {
    private string _phoneNumber;
    private T _payment;
    private double _purchaseAmount;

    public Purchase(string phoneNumber, T payment, double purchaseAmount)
    {
        _phoneNumber = phoneNumber;
        _payment = payment;
        _purchaseAmount = purchaseAmount;
    }
    public string GetPurchaseInfo() {
        return $"Номер телефона: {_phoneNumber}\n{_payment}\nСумма покупки: {_purchaseAmount} руб.";
    }

    public static void Run() {
        Console.WriteLine("Оформление покупки");

        Console.Write("Введите номер телефона покупателя: ");
        string phone = Console.ReadLine();

        Console.Write("Введите сумму покупки (руб): ");
        double amount = double.Parse(Console.ReadLine());

        Console.WriteLine("Выберите способ оплаты:");
        Console.WriteLine("1) Безналичный");
        Console.WriteLine("2) Наличный");
        Console.Write("Ваш выбор: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1) {
            Console.Write("Введите номер карты: ");
            string? cardNumber = Console.ReadLine();
            Console.Write("Введите дату выдачи (MM/YY): ");
            string? expiryDate = Console.ReadLine();
            Console.Write("Введите ФИО владельца: ");
            string? fullName = Console.ReadLine();
            Console.Write("Введите CVC-код: ");
            int cvc = int.Parse(Console.ReadLine());

            NonCash nonCash = new NonCash(amount, cardNumber, expiryDate, fullName, cvc);
            Purchase<NonCash> purchase = new Purchase<NonCash>(phone, nonCash, amount);
            Console.WriteLine("Информация о покупке");
            Console.WriteLine(purchase.GetPurchaseInfo());
        }
        else if (choice == 2) {
            Console.Write("Введите сумму сдачи (руб): ");
            double change = double.Parse(Console.ReadLine());

            Cash cash = new Cash(amount, change);
            Purchase<Cash> purchase = new Purchase<Cash>(phone, cash, amount);
            Console.WriteLine("Информация о покупке");
            Console.WriteLine(purchase.GetPurchaseInfo());
        }
        else 
            Console.WriteLine("Неверный выбор!");
    }
}
