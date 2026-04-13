namespace Project;
using System;

public class PublicTransport {
    protected static string Number;
    protected static int Capacity;
    protected static double CurrentSpeed;

    public PublicTransport(string number, int capacity, double currentSpeed) {
        Number = number;
        Capacity = capacity;
        CurrentSpeed = currentSpeed;
    }

    public virtual string GetInfo() {
        return $"Номер: {Number}, Вместимость: {Capacity} чел., Текущая скорость: {CurrentSpeed} км/ч";
    }
}
