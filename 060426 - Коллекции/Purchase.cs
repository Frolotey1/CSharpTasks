using System;
using System.Collections.Generic;

public interface IPayment {
    static string PaymentMethod { get; }
    double Amount { get; set; }          
}

public struct NonCash : IPayment {
    public static string PaymentMethod = "Безналичный";
    public double Amount { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string FullName { get; set; }
    public int CVC { get; set; }

    public NonCash(double amount, string cardNumber, string expiryDate, string fullName, int cvc) {
        Amount = amount;
        CardNumber = cardNumber;
        ExpiryDate = expiryDate;
        FullName = fullName;
        CVC = cvc;
    }

    public override string ToString() {
        return $"Способ оплаты: {PaymentMethod}, Сумма: {Amount} руб., Карта: {CardNumber}, Срок: {ExpiryDate}, Владелец: {FullName}, CVC: {CVC}";
    }
}

public struct Cash : IPayment {
    public static string PaymentMethod = "Наличный";
    public double Amount { get; set; }
    public double Change { get; set; } 

    public Cash(double amount, double change) {
        Amount = amount;
        Change = change;
    }

    public override string ToString() {
        return $"Способ оплаты: {PaymentMethod}, Сумма: {Amount} руб., Сдача: {Change} руб.";
    }
}

public class Purchase<T> where T : IPayment {
    private string PhoneNumber;
    private T Payment;
    private double PurchaseAmount;

    public Purchase(string phoneNumber, T payment, double purchaseAmount) {
        PhoneNumber = phoneNumber;
        Payment = payment;
        PurchaseAmount = purchaseAmount;
    }

    public string GetPurchaseInfo() {
        return $"Номер телефона: {PhoneNumber}\n{Payment}\nСумма покупки: {PurchaseAmount} руб.";
    }

    public static void Run() {
        Console.Write("Введите номер телефона покупателя: ");
        string phone = Console.ReadLine();

        Console.Write("Введите сумму покупки в рублях: ");
        double amount = double.Parse(Console.ReadLine());

        Console.WriteLine("Выберите способ оплаты:");
        Console.WriteLine("1) Безналичный");
        Console.WriteLine("2) Наличный");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1) {
            Console.Write("Введите номер карты: ");
            string cardNumber = Console.ReadLine();
            Console.Write("Введите дату выдачи (MM/YY): ");
            string expiryDate = Console.ReadLine();
            Console.Write("Введите ФИО владельца: ");
            string fullName = Console.ReadLine();
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
        else {
            Console.WriteLine("Неверный выбор");
        }
    }
}
