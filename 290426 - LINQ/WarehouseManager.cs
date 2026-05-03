using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartWarehouse;

public delegate void LowStockAlertHandler(string itemName, int currentQuantity);

public class WarehouseManager<T> where T : class, IInventoryItem {
    private Dictionary<string, T> items = new Dictionary<string, T>();

    public event LowStockAlertHandler OnLowStock;

    public void Add(T item) {
        if (items.ContainsKey(item.Name)) {
            Console.WriteLine("Товар '" + item.Name + "' уже существует в системе.");
            return;
        }

        items.Add(item.Name, item);
        Console.WriteLine("Добавлен товар: " + item.Name + ", количество: " + item.Quantity);
    }

    public bool Remove(string name) {
        if (items.Remove(name)) {
            Console.WriteLine("Товар '" + name + "' удалён из системы");
            return true;
        }

        Console.WriteLine("Товар '" + name + "' не найден");
        return false;
    }

    public void UpdateQuantity(string name, int newQuantity) {
        if (!items.TryGetValue(name, out T item)) {
            Console.WriteLine("Товар '" + name + "' не найден");
            return;
        }

        if (newQuantity < 0) {
            Console.WriteLine("Ошибка: количество не может быть отрицательным");
            return;
        }

        int oldQuantity = item.Quantity;
        item.Quantity = newQuantity;

        Console.WriteLine("Количество товара '" + name + "' изменено: c " + oldQuantity + " на " + newQuantity);

        if (newQuantity <= 5) {
            OnLowStock?.Invoke(name, newQuantity);
        }
    }

    public T GetItem(string name) {
        items.TryGetValue(name, out T item);
        return item;
    }

    public IEnumerable<T> GetAllItems() {
        return items.Values;
    }

    public IEnumerable<T> GetLowStockItems(int threshold) {
        return items.Values.Where(item => item.Quantity <= threshold).OrderBy(item => item.Name);
    }

    public IEnumerable<IGrouping<string, T>> GetItemsByCategory() {
        return items.Values.GroupBy(item => item.Category.Name);
    }

    public decimal GetTotalInventoryValue() {
        return items.Values.Sum(item => item.Price * item.Quantity);
    }

    public IEnumerable<string> GetTopCategoriesByValue(int count) {
        if (count <= 0) {
            return Enumerable.Empty<string>();
        }

        return items.Values
            .GroupBy(item => item.Category.Name)
            .Select(group => new { CategoryName = group.Key, TotalValue = group.Sum(item => item.Price * item.Quantity) })
            .OrderByDescending(c => c.TotalValue)
            .Take(count)
            .Select(c => c.CategoryName);
    }
}
