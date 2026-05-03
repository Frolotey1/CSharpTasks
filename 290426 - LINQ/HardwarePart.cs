using System;

namespace SmartWarehouse;

public class HardwarePart : IInventoryItem {
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; set; }
    public CategoryInfo Category { get; private set; }
    public string Manufacturer { get; private set; }
    public string Model { get; private set; }

    public HardwarePart(string name, decimal price, int quantity, CategoryInfo category, string manufacturer, string model) {
        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Ошибка: название детали не может быть пустым");
            Name = "Unknown Part";
            Price = 0;
            Quantity = 0;
            Category = new CategoryInfo("Unknown", "UNK");
            Manufacturer = "Unknown";
            Model = "Unknown";
            return;
        }

        if (price < 0) {
            Console.WriteLine("Ошибка: цена детали не может быть отрицательной. Установлена цена 0");
            Price = 0;
        } else {
            Price = price;
        }

        if (quantity < 0) {
            Console.WriteLine("Ошибка: количество детали не может быть отрицательным. Установлено 0");
            Quantity = 0;
        } else {
            Quantity = quantity;
        }

        if (string.IsNullOrWhiteSpace(manufacturer)) {
            Console.WriteLine("Ошибка: производитель детали не указан. Установлено 'Unknown'");
            Manufacturer = "Unknown";
        } else {
            Manufacturer = manufacturer;
        }

        if (string.IsNullOrWhiteSpace(model)) {
            Console.WriteLine("Ошибка: модель детали не указана. Установлено 'Unknown'");
            Model = "Unknown";
        } else {
            Model = model;
        }

        Name = name;
        Category = category;
    }
}
