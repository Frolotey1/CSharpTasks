using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartWarehouse;

public class Program {
    private static WarehouseManager<IInventoryItem> warehouse = new WarehouseManager<IInventoryItem>();
    private static CategoryInfo booksCategory;
    private static CategoryInfo electronicsCategory;
    private static CategoryInfo toolsCategory;
    private static List<CategoryInfo> customCategories = new List<CategoryInfo>();
    private static bool categoriesInitialized = false;

    private static void EnsureCategoriesInitialized() {
        if (!categoriesInitialized) {
            Console.WriteLine("Перед добавлением товаров необходимо создать категории");
            booksCategory = CreateCategory("Книги");
            electronicsCategory = CreateCategory("Электроника");
            toolsCategory = CreateCategory("Инструменты");
            categoriesInitialized = true;
        }
    }

    private static CategoryInfo CreateCategory(string defaultName) {
        Console.Write("Введите название категории (Enter - '" + defaultName + "'): ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) name = defaultName;

        Console.Write("Введите код категории (3 буквы) для '" + name + "': ");
        string code = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3) {
            code = defaultName.Substring(0, Math.Min(3, defaultName.Length)).ToUpper();
            Console.WriteLine("Установлен код по умолчанию: " + code);
        }

        CategoryInfo newCategory = new CategoryInfo(name, code);
        
        if (defaultName != "Книги" && defaultName != "Электроника" && defaultName != "Инструменты" && defaultName != "Новая категория") {
            customCategories.Add(newCategory);
        }
        
        return newCategory;
    }

    private static void ShowAllCustomCategories() {
        if (customCategories.Count == 0) {
            Console.WriteLine("Нет созданных пользователем категорий.");
            return;
        }
        
        Console.WriteLine("Созданные пользователем категории:");
        for (int i = 0; i < customCategories.Count; i++) {
            Console.WriteLine((i + 1) + ") " + customCategories[i]);
        }
    }

    private static void CreateNewCustomCategory() {
        Console.Write("Введите название категории: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        Console.Write("Введите код категории (3 буквы): ");
        string code = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(code) || code.Length != 3) {
            code = name.Substring(0, Math.Min(3, name.Length)).ToUpper();
            Console.WriteLine("Установлен код по умолчанию: " + code);
        }

        CategoryInfo newCategory = new CategoryInfo(name, code);
        customCategories.Add(newCategory);
        Console.WriteLine("Категория '" + name + "' успешно создана!");
    }

    private static void AddBook() {
        EnsureCategoriesInitialized();

        Console.Write("Введите название книги: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        Console.Write("Введите цену книги в рублях: ");
        decimal price = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        if (price < 0.0) {
            Console.WriteLine("Цена не может быть отрицательной!");
            return;
        }

        Console.Write("Введите количество: ");
        int quantity = int.Parse(Console.ReadLine());
        if (quantity < 0) {
            Console.WriteLine("Количество не может быть отрицательным!");
            return;
        }

        Console.Write("Введите автора: ");
        string author = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(author)) {
            author = "Unknown";
        }

        Console.Write("Введите количество страниц: ");
        int pages = int.Parse(Console.ReadLine());
        if (pages < 0) {
            pages = 0;
        }

        CategoryInfo category = SelectCategory();

        Book book = new Book(name, price, quantity, category, author, pages);
        warehouse.Add(book);
    }

    private static void AddHardwarePart() {
        EnsureCategoriesInitialized();

        Console.Write("Введите название детали: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        Console.Write("Введите цену детали (руб): ");
        decimal price = decimal.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        if (price < 0) {
            Console.WriteLine("Цена не может быть отрицательной!");
            return;
        }

        Console.Write("Введите количество: ");
        int quantity = int.Parse(Console.ReadLine());
        if (quantity < 0) {
            Console.WriteLine("Количество не может быть отрицательным!");
            return;
        }

        Console.Write("Введите производителя: ");
        string manufacturer = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(manufacturer)) {
            manufacturer = "Unknown";
        }

        Console.Write("Введите модель: ");
        string model = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(model)) {
            model = "Unknown";
        }

        CategoryInfo category = SelectCategory();

        HardwarePart part = new HardwarePart(name, price, quantity, category, manufacturer, model);
        warehouse.Add(part);
    }

    private static CategoryInfo SelectCategory() {
        Console.WriteLine("1) Книги");
        Console.WriteLine("2) Электроника");
        Console.WriteLine("3) Инструменты");
        
        int customStartIndex = 4;
        if (customCategories.Count > 0) {
            Console.WriteLine("Созданные категории: ");
            for (int i = 0; i < customCategories.Count; i++) {
                Console.WriteLine((customStartIndex + i) + ") " + customCategories[i]);
            }
            customStartIndex = customStartIndex + customCategories.Count;
        }
        
        Console.WriteLine(customStartIndex + ") Создать новую категорию");
        Console.Write("Выберите категорию: ");

        int choice = int.Parse(Console.ReadLine());

        if (customCategories.Count > 0 && choice >= 4 && choice < 4 + customCategories.Count) {
            int categoryIndex = choice - 4;
            return customCategories[categoryIndex];
        }
        else if (choice == 4 + customCategories.Count || (customCategories.Count == 0 && choice == 4)) {
            CreateNewCustomCategory();
            if (customCategories.Count > 0) {
                return customCategories[customCategories.Count - 1];
            }
            Console.Write("Введите название категории: ");
            string newName = Console.ReadLine();
            Console.Write("Введите код категории (3 буквы): ");
            string newCode = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newCode) || newCode.Length != 3) {
                newCode = newName?.Substring(0, Math.Min(3, newName?.Length ?? 3)).ToUpper() ?? "NEW";
            }
            return new CategoryInfo(newName ?? "Новая категория", newCode);
        }
        switch (choice) {
            case 1:
                return booksCategory ?? new CategoryInfo("Книги", "BK");
            case 2:
                return electronicsCategory ?? new CategoryInfo("Электроника", "EL");
            case 3:
                return toolsCategory ?? new CategoryInfo("Инструменты", "TL");
            default:
                Console.WriteLine("Выбрана категория по умолчанию: Другое");
                return new CategoryInfo("Другое", "OTH");
        }
    }

    private static void ShowAllItems() {
        List<IInventoryItem> items = warehouse.GetAllItems().ToList();

        if (items.Count == 0) {
            Console.WriteLine("Склад пуст.");
            return;
        }

        foreach (IInventoryItem item in items) {
            if (item is Book book) {
                Console.WriteLine("[Книга] " + book.Name + " | Автор: " + book.Author + " | Страниц: " + book.Pages + " | " + book.Price + " руб. | " + book.Quantity + " шт. | Категория: " + book.Category);
            } else if (item is HardwarePart part) {
                Console.WriteLine("[Деталь] " + part.Name + " | Производитель: " + part.Manufacturer + " | Модель: " + part.Model + " | " + part.Price + " руб. | " + part.Quantity + " шт. | Категория: " + part.Category);
            }
        }
    }

    private static void UpdateQuantity() {
        Console.Write("Введите название товара: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        Console.Write("Введите новое количество: ");
        int quantity = int.Parse(Console.ReadLine());
        if (quantity < 0) {
            Console.WriteLine("Количество не может быть отрицательным!");
            return;
        }

        warehouse.UpdateQuantity(name, quantity);
    }

    private static void RemoveItem() {
        Console.Write("Введите название товара для удаления: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Название не может быть пустым!");
            return;
        }

        warehouse.Remove(name);
    }

    private static void LowStockAlertHandler(string itemName, int currentQuantity) {
        Console.WriteLine("Низкий остаток! Товар '" + itemName + "' | осталось " + currentQuantity + " шт. меньше 5)");
    }

    private static void ManageCategories() {
        if (!categoriesInitialized) {
            booksCategory = CreateCategory("Книги");
            electronicsCategory = CreateCategory("Электроника");
            toolsCategory = CreateCategory("Инструменты");
            categoriesInitialized = true;
            Console.WriteLine("Категории созданы успешно!");
            return;
        }

        Console.WriteLine("Управление категориями");
        Console.WriteLine("1) Показать текущие категории (стандартные + созданные)");
        Console.WriteLine("2) Изменить категорию 'Книги'");
        Console.WriteLine("3) Изменить категорию 'Электроника'");
        Console.WriteLine("4) Изменить категорию 'Инструменты'");
        Console.WriteLine("5) Создать новую категорию");
        Console.WriteLine("6) Показать все созданные пользователем категории");
        Console.WriteLine("7) Назад");
        Console.Write("Выберите действие: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice) {
            case 1:
                Console.WriteLine("Стандартные категории:");
                Console.WriteLine("Книги: " + booksCategory);
                Console.WriteLine("Электроника: " + electronicsCategory);
                Console.WriteLine("Инструменты: " + toolsCategory);
                ShowAllCustomCategories();
                break;
            case 2:
                booksCategory = CreateCategory("Книги");
                break;
            case 3:
                electronicsCategory = CreateCategory("Электроника");
                break;
            case 4:
                toolsCategory = CreateCategory("Инструменты");
                break;
            case 5:
                CreateNewCustomCategory();
                break;
            case 6:
                ShowAllCustomCategories();
                break;
            case 7:
                return;
            default:
                Console.WriteLine("Неверный выбор!");
                break;
        }
    }

    private static void ShowAnalytics() {
        Console.WriteLine("1. Товары с остатком меньше 5: ");
        List<IInventoryItem> lowStockItems = warehouse.GetLowStockItems(5).ToList();
        if (lowStockItems.Count == 0) {
            Console.WriteLine("Нет товаров с низким остатком.");
        } else {
            foreach (IInventoryItem item in lowStockItems) {
                Console.WriteLine(item.Name + ": " + item.Quantity + " шт.");
            }
        }

        Console.WriteLine("2. Группировка товаров по категориям:");
        var categories = warehouse.GetItemsByCategory().ToList();
        if (categories.Count == 0) {
            Console.WriteLine("Нет товаров для группировки.");
        } else {
            foreach (var group in categories) {
                Console.WriteLine("Категория: " + group.Key);
                foreach (IInventoryItem item in group) {
                    Console.WriteLine(item.Name + ": " + item.Quantity + " шт. | " + item.Price + " руб.");
                }
            }
        }

        Console.WriteLine("3. Общая стоимость всех товаров на складе: " + warehouse.GetTotalInventoryValue() + " руб.");

        Console.WriteLine("4. Топ 3 категории с наибольшей стоимостью товаров:");
        List<string> topCategories = warehouse.GetTopCategoriesByValue(3).ToList();
        if (topCategories.Count == 0) {
            Console.WriteLine("Нет категорий для отображения.");
        } else {
            int rank = 1;
            foreach (string category in topCategories) {
                Console.WriteLine(rank + ". " + category);
                rank++;
            }
        }
    }

    private static void PrintMenu() {
        Console.WriteLine("1) Добавить книгу");
        Console.WriteLine("2) Добавить аппаратную деталь");
        Console.WriteLine("3) Показать все товары");
        Console.WriteLine("4) Изменить количество товара");
        Console.WriteLine("5) Удалить товар");
        Console.WriteLine("6) Аналитика склада");
        Console.WriteLine("7) Управление категориями");
        Console.WriteLine("8) Выход");
        Console.Write("Выберите действие: ");
    }

    public static void Main() {
        warehouse.OnLowStock += LowStockAlertHandler;

        int choice = 0;

        while (choice != 8) {
            PrintMenu();
            choice = int.Parse(Console.ReadLine());

            switch (choice) {
                case 1:
                    AddBook();
                    break;
                case 2:
                    AddHardwarePart();
                    break;
                case 3:
                    ShowAllItems();
                    break;
                case 4:
                    UpdateQuantity();
                    break;
                case 5:
                    RemoveItem();
                    break;
                case 6:
                    ShowAnalytics();
                    break;
                case 7:
                    ManageCategories();
                    break;
                case 8:
                    Console.WriteLine("Завершение программы");
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}
