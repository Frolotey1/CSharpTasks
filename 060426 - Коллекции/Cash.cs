namespace Project;
using System;

public struct Cash : IPayment {
    private double _amount;
    private double _change;

    public static string PaymentMethod => "Наличный";

    public double Amount {
        get => _amount;
        set => _amount = value;
    }

    public Cash(double amount, double change) {
        _amount = amount;
        _change = change;
    }

    public override string ToString() {
        return $"Способ оплаты: {PaymentMethod} | Сумма: {_amount} руб. | Сдача: {_change} руб.";
    }
}
