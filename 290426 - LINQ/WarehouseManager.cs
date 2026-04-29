using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _290426___LINQ {
    public delegate void LowStockAlertHandler(string itemName, int currentQuantity);

    public class WarehouseManager<T> where T : class, IInventoryItem {
        private readonly Dictionary<string, T> _items = new();

        public event LowStockAlertHandler? OnLowStock;

        public void Add(T item) {
            if (_items.ContainsKey(item.Name)) {
                Console.WriteLine($"Товар '{item.Name}' уже существует в системе. Используйте UpdateQuantity для изменения количества");
                return;
            }

            _items.Add(item.Name, item);
            Console.WriteLine($"Добавлен товар: {item.Name}, количество: {item.Quantity}");
        }

        public bool Remove(string name) {
            if (_items.Remove(name)) {
                Console.WriteLine($"Товар '{name}' удалён из системы");
                return true;
            }

            Console.WriteLine($"Товар '{name}' не найден");
            return false;
        }

        public void UpdateQuantity(string name, int newQuantity) {
            if (!_items.TryGetValue(name, out T? item)) {
                Console.WriteLine($"Товар '{name}' не найден");
                return;
            }

            if (newQuantity < 0) {
                Console.WriteLine($"Ошибка: количество не может быть отрицательным. Количество не изменено");
                return;
            }

            int oldQuantity = item.Quantity;
            item.Quantity = newQuantity;

            Console.WriteLine($"Количество товара '{name}' изменено: {oldQuantity} → {newQuantity}");

            if (newQuantity <= 5) {
                OnLowStock?.Invoke(name, newQuantity);
            }
        }

        public T? GetItem(string name) {
            _items.TryGetValue(name, out T? item);
            return item;
        }

        public IEnumerable<T> GetAllItems()
        {
            return _items.Values;
        }

        public IEnumerable<T> GetLowStockItems(int threshold) {
            return _items.Values
                .Where(item => item.Quantity <= threshold)
                .OrderBy(item => item.Name);
        }

        public IEnumerable<IGrouping<string, T>> GetItemsByCategory() {
            return _items.Values
                .GroupBy(item => item.Category.Name);
        }

        public decimal GetTotalInventoryValue() {
            return _items.Values
                .Sum(item => item.Price * item.Quantity);
        }

        public IEnumerable<string> GetTopCategoriesByValue(int count) {
            if (count <= 0)
            {
                return Enumerable.Empty<string>();
            }

            return _items.Values
                .GroupBy(item => item.Category.Name)
                .Select(group => new
                {
                    CategoryName = group.Key,
                    TotalValue = group.Sum(item => item.Price * item.Quantity)
                })
                .OrderByDescending(c => c.TotalValue)
                .Take(count)
                .Select(c => c.CategoryName);
        }
    }
}
