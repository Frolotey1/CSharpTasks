namespace Project;
using System;

public struct NonCash : IPayment {
    private double _amount;
    private string _cardNumber;
    private string _expiryDate;
    private string _fullName;
    private int _cvc;

    public static string PaymentMethod => "Безналичный";

    public double Amount {
        get => _amount;
        set => _amount = value;
    }

    public NonCash(double amount, string cardNumber, string expiryDate, string fullName, int cvc) {
        _amount = amount;
        _cardNumber = cardNumber;
        _expiryDate = expiryDate;
        _fullName = fullName;
        _cvc = cvc;
    }

    public override string ToString() {
        return $"Способ оплаты: {PaymentMethod} | Сумма: {_amount} руб. | Карта: {_cardNumber} | Срок: {_expiryDate} | Владелец: {_fullName} | CVC: {_cvc}";
    }
}
