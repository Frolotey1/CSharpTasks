using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _290426___LINQ {
    public class Book : IInventoryItem {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; set; }
        public CategoryInfo Category { get; }
        public string Author { get; }
        public int Pages { get; }

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
                Console.WriteLine($"Ошибка: цена книги '{name}' не может быть отрицательной. Установлена цена 0");
                Price = 0;
            }
            else {
                Price = price;
            }

            if (quantity < 0) {
                Console.WriteLine($"Ошибка: количество книги '{name}' не может быть отрицательным. Установлено 0");
                Quantity = 0;
            }
            else
            {
                Quantity = quantity;
            }

            if (string.IsNullOrWhiteSpace(author)) {
                Console.WriteLine($"Ошибка: автор книги '{name}' не указан. Установлено 'Unknown'");
                Author = "Unknown";
            }
            else {
                Author = author;
            }

            if (pages < 0) {
                Console.WriteLine($"Ошибка: количество страниц книги '{name}' не может быть отрицательным. Установлено 0");
                Pages = 0;
            }
            else {
                Pages = pages;
            }

            Name = name;
            Category = category;
        }
    }
}
