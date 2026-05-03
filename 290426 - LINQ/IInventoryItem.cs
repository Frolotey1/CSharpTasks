namespace SmartWarehouse;

public interface IInventoryItem {
    string Name { get; }
    decimal Price { get; }
    int Quantity { get; set; }
    CategoryInfo Category { get; }
}
