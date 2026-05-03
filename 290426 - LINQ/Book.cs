using System;

namespace SmartWarehouse;

public class Book : IInventoryItem {
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; set; }
    public CategoryInfo Category { get; private set; }
    public string Author { get; private set; }
    public int Pages { get; private set; }

    public Book(string name, decimal price, int quantity, CategoryInfo category, string author, int pages) {
        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Ошибка: название книги не может быть пустым");
            Name = "Unknown Book";
            Price = 0;
            Quantity = 0;
            Category = new CategoryInfo("Unknown", "UNK");
            Author = "Unknown";
            Pages = 0;
            return;
        }

        if (price < 0) {
            Console.WriteLine("Ошибка: цена книги не может быть отрицательной. Установлена цена 0");
            Price = 0;
        } else {
            Price = price;
        }

        if (quantity < 0) {
            Console.WriteLine("Ошибка: количество книги не может быть отрицательным. Установлено 0");
            Quantity = 0;
        } else {
            Quantity = quantity;
        }

        if (string.IsNullOrWhiteSpace(author)) {
            Console.WriteLine("Ошибка: автор книги не указан. Установлено 'Unknown'");
            Author = "Unknown";
        } else {
            Author = author;
        }

        if (pages < 0) {
            Console.WriteLine("Ошибка: количество страниц не может быть отрицательным. Установлено 0");
            Pages = 0;
        } else {
            Pages = pages;
        }

        Name = name;
        Category = category;
    }
}
