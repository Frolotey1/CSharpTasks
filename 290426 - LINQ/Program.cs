using System.Globalization;

namespace _290426___LINQ
{
    public class Program
    {
        private static WarehouseManager<IInventoryItem> _warehouse = new();
        private static CategoryInfo? _booksCategory;
        private static CategoryInfo? _electronicsCategory;
        private static CategoryInfo? _toolsCategory;
        private static bool _categoriesInitialized = false;

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Добавить книгу");
                Console.WriteLine("2) Добавить аппаратную деталь");
                Console.WriteLine("3) Показать все товары");
                Console.WriteLine("4) Изменить количество товара");
                Console.WriteLine("5) Удалить товар");
                Console.WriteLine("6) Аналитика склада");
                Console.WriteLine("7) Управление категориями");
                Console.WriteLine("8) Выход");
                Console.Write("\nВыберите действие: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
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
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        private static void EnsureCategoriesInitialized()
        {
            if (!_categoriesInitialized)
            {
                Console.Clear();
                Console.WriteLine("Перед добавлением товаров необходимо создать категории");
                _booksCategory = CreateCategory("Книги");
                _electronicsCategory = CreateCategory("Электроника");
                _toolsCategory = CreateCategory("Инструменты");
                _categoriesInitialized = true;
            }
        }

        private static CategoryInfo CreateCategory(string defaultName)
        {
            Console.Write($"Введите название категории (Enter - '{defaultName}'): ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = defaultName;

            Console.Write($"Введите код категории (3 буквы) для '{name}': ");
            string? code = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(code) || code.Length != 3)
            {
                code = defaultName.Substring(0, Math.Min(3, defaultName.Length)).ToUpper();
                Console.WriteLine($"Установлен код по умолчанию: {code}");
            }

            return new CategoryInfo(name, code);
        }

        private static void AddBook()
        {
            EnsureCategoriesInitialized();

            Console.Clear();

            Console.Write("Введите название книги: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым!");
                return;
            }

            Console.Write("Введите цену книги (руб): ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || price < 0)
            {
                Console.WriteLine("Некорректная цена!");
                return;
            }

            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Некорректное количество!");
                return;
            }

            Console.Write("Введите автора: ");
            string? author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                author = "Unknown";
            }

            Console.Write("Введите количество страниц: ");
            if (!int.TryParse(Console.ReadLine(), out int pages) || pages < 0)
            {
                pages = 0;
            }

            CategoryInfo category = SelectCategory();
            if (category.Name == "Unknown")
            {
                Console.WriteLine("Создайте категорию через пункт 'Управление категориями'");
                return;
            }

            Book book = new Book(name, price, quantity, category, author, pages);
            _warehouse.Add(book);
        }

        private static void AddHardwarePart()
        {
            EnsureCategoriesInitialized();

            Console.Clear();

            Console.Write("Введите название детали: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым!");
                return;
            }

            Console.Write("Введите цену детали (руб): ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price) || price < 0)
            {
                Console.WriteLine("Некорректная цена!");
                return;
            }

            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Некорректное количество!");
                return;
            }

            Console.Write("Введите производителя: ");
            string? manufacturer = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(manufacturer))
            {
                manufacturer = "Unknown";
            }

            Console.Write("Введите модель: ");
            string? model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model))
            {
                model = "Unknown";
            }

            CategoryInfo category = SelectCategory();
            if (category.Name == "Unknown")
            {
                Console.WriteLine("Создайте категорию через пункт 'Управление категориями'");
                return;
            }

            HardwarePart part = new HardwarePart(name, price, quantity, category, manufacturer, model);
            _warehouse.Add(part);
        }

        private static CategoryInfo SelectCategory()
        {
            Console.WriteLine("Выберите категорию:");
            Console.WriteLine("1) Книги");
            Console.WriteLine("2) Электроника");
            Console.WriteLine("3) Инструменты");
            Console.WriteLine("4) Создать новую категорию");
            Console.Write("Ваш выбор: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    return _booksCategory ?? new CategoryInfo("Книги", "BK");
                case 2:
                    return _electronicsCategory ?? new CategoryInfo("Электроника", "EL");
                case 3:
                    return _toolsCategory ?? new CategoryInfo("Инструменты", "TL");
                case 4:
                    Console.Write("Введите название категории: ");
                    string? newName = Console.ReadLine();
                    Console.Write("Введите код категории (3 буквы): ");
                    string? newCode = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newCode) || newCode.Length != 3)
                    {
                        newCode = newName?.Substring(0, Math.Min(3, newName?.Length ?? 3)).ToUpper() ?? "NEW";
                    }
                    return new CategoryInfo(newName ?? "Новая категория", newCode);
                default:
                    Console.WriteLine("Выбрана категория по умолчанию: Другое");
                    return new CategoryInfo("Другое", "OTH");
            }
        }

        private static void ShowAllItems()
        {
            Console.Clear();

            var items = _warehouse.GetAllItems().ToList();

            if (items.Count == 0)
            {
                Console.WriteLine("Склад пуст.");
                return;
            }

            foreach (var item in items)
            {
                if (item is Book book)
                {
                    Console.WriteLine($"[Книга] {book.Name} | Автор: {book.Author} | Страниц: {book.Pages} | {book.Price} руб. | {book.Quantity} шт. | Категория: {book.Category}");
                }
                else if (item is HardwarePart part)
                {
                    Console.WriteLine($"[Деталь] {part.Name} | Производитель: {part.Manufacturer} | Модель: {part.Model} | {part.Price} руб. | {part.Quantity} шт. | Категория: {part.Category}");
                }
            }
        }

        private static void UpdateQuantity()
        {
            Console.Clear();

            Console.Write("Введите название товара: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым!");
                return;
            }

            Console.Write("Введите новое количество: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
            {
                Console.WriteLine("Некорректное количество!");
                return;
            }

            _warehouse.UpdateQuantity(name, quantity);
        }

        private static void RemoveItem()
        {
            Console.Clear();

            Console.Write("Введите название товара для удаления: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым!");
                return;
            }

            _warehouse.Remove(name);
        }

        private static void ShowAnalytics() {
            Console.Clear();

            Console.WriteLine("1. Товары с остатком ≤ 5 (отсортированы по имени):");
            var lowStockItems = _warehouse.GetLowStockItems(5).ToList();
            if (lowStockItems.Count == 0)
            {
                Console.WriteLine("Нет товаров с низким остатком.");
            }
            else
            {
                foreach (var item in lowStockItems)
                {
                    Console.WriteLine($"Название: {item.Name}: {item.Quantity} шт.");
                }
            }

            Console.WriteLine("2. Группировка товаров по категориям:");
            var categories = _warehouse.GetItemsByCategory().ToList();
            if (categories.Count == 0)
            {
                Console.WriteLine("Нет товаров для группировки.");
            }
            else
            {
                foreach (var group in categories)
                {
                    Console.WriteLine($"Категория: {group.Key}");
                    foreach (var item in group)
                    {
                        Console.WriteLine($"Название: {item.Name}: {item.Quantity} шт., {item.Price} руб.");
                    }
                }
            }

            Console.WriteLine($"3. Общая стоимость всех товаров на складе: {_warehouse.GetTotalInventoryValue()} руб.");

            Console.WriteLine("4. Топ-3 категории с наибольшей стоимостью товаров:");
            var topCategories = _warehouse.GetTopCategoriesByValue(3).ToList();
            if (topCategories.Count == 0)
            {
                Console.WriteLine("Нет категорий для отображения.");
            }
            else
            {
                int rank = 1;
                foreach (var category in topCategories)
                {
                    Console.WriteLine($"   {rank}. {category}");
                    rank++;
                }
            }
        }

        private static void ManageCategories()
        {
            if (!_categoriesInitialized)
            {
                Console.Clear();
                _booksCategory = CreateCategory("Книги");
                _electronicsCategory = CreateCategory("Электроника");
                _toolsCategory = CreateCategory("Инструменты");
                _categoriesInitialized = true;
                Console.WriteLine("Категории созданы успешно!");
                return;
            }

            Console.Clear();
            Console.WriteLine("1) Показать текущие категории");
            Console.WriteLine("2) Изменить категорию 'Книги'");
            Console.WriteLine("3) Изменить категорию 'Электроника'");
            Console.WriteLine("4) Изменить категорию 'Инструменты'");
            Console.WriteLine("5) Создать новую категорию");
            Console.WriteLine("6) Назад");
            Console.Write("\nВыберите действие: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowCategories();
                    break;
                case 2:
                    _booksCategory = CreateCategory("Книги");
                    break;
                case 3:
                    _electronicsCategory = CreateCategory("Электроника");
                    break;
                case 4:
                    _toolsCategory = CreateCategory("Инструменты");
                    break;
                case 5:
                    var newCategory = CreateCategory("Новая категория");
                    Console.WriteLine($"\nСоздана новая категория: {newCategory}");
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
            ManageCategories();
        }

        private static void ShowCategories()
        {
            Console.Clear();
            Console.WriteLine($"Книги: {_booksCategory}");
            Console.WriteLine($"Электроника: {_electronicsCategory}");
            Console.WriteLine($"Инструменты: {_toolsCategory}");
            Console.WriteLine();
        }

        private static void LowStockAlertHandler(string itemName, int currentQuantity)
        {
            Console.WriteLine($" Низкий остаток! Товар '{itemName}' осталось {currentQuantity} шт. (≤5)");
        }

        public static void Main()
        {
            _warehouse.OnLowStock += LowStockAlertHandler;
            Run();
        }
    }
}