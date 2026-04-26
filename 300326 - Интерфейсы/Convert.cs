namespace Project;
using System;
using System.Globalization;

public interface IConverter {
    string FromScale { get; set; }
    string ToScale { get; set; }
    double Convert(double value);
}

public interface IOutputConsole {
    void Print();
}

public class CelsiusToFahrenheit : IConverter, IOutputConsole {
    public string FromScale { get; set; }
    public string ToScale { get; set; }

    public CelsiusToFahrenheit() {
        FromScale = "Цельсий";
        ToScale = "Фаренгейт";
    }
    public double Convert(double value) {
        return 1.8 * value + 32;
    }
    public void Print() {
        Console.WriteLine($"Конвертация из шкалы: {FromScale} в шкалу: {ToScale}");
    }
}

public class CelsiusToKelvin : IConverter, IOutputConsole {
    public string FromScale { get; set; }
    public string ToScale { get; set; }

    public CelsiusToKelvin() {
        FromScale = "Цельсий";
        ToScale = "Кельвин";
    }
    public double Convert(double value) {
        return 273.15 + value;
    }
    public void Print() {
        Console.WriteLine($"Конвертация из шкалы: {FromScale} в шкалу: {ToScale}");
    }
}

public class Converter {
    public static void Run() {
        Console.Write("1) Из Цельсия в Фаренгейт\n2) Из Цельсия в Кельвин\nВыберите тип конвертации: ");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Введите значение в градусах Цельсия: ");
        double celsius = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        if (choice == 1) {
            CelsiusToFahrenheit converter = new CelsiusToFahrenheit();
            converter.Print();
            double result = converter.Convert(celsius);
            Console.WriteLine($"{celsius}°C = {result}°F");
        }
        else if (choice == 2) {
            CelsiusToKelvin converter = new CelsiusToKelvin();
            converter.Print();
            double result = converter.Convert(celsius);
            Console.WriteLine($"{celsius}°C = {result}K");
        }
	else {
            Console.WriteLine("Неверный выбор!");
        }
    }
}
