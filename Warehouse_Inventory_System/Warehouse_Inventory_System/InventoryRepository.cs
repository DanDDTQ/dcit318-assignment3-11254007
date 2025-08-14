using System;

namespace WarehouseInventorySystem;

public class InventoryRepository<T> where T : IInventoryItem
{
    private readonly Dictionary<int, T> _items = new();

    public void AddItem(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        if (_items.ContainsKey(item.Id))
            throw new DuplicateItemException($"An item with ID {item.Id} already exists.");

        _items[item.Id] = item;
    }

    public T GetItemById(int id)
    {
        if (!_items.TryGetValue(id, out var item))
            throw new ItemNotFoundException($"No item found with ID {id}.");

        return item;
    }

    public void RemoveItem(int id)
    {
        if (!_items.Remove(id))
            throw new ItemNotFoundException($"Cannot remove: no item found with ID {id}.");
    }

    public List<T> GetAllItems()
    {
        // Return a copy list to avoid external mutation of the internal collection
        return new List<T>(_items.Values);
    }

    public void UpdateQuantity(int id, int newQuantity)
    {
        if (newQuantity < 0)
            throw new InvalidQuantityException("Quantity cannot be negative.");

        if (!_items.TryGetValue(id, out var item))
            throw new ItemNotFoundException($"No item found with ID {id}.");

        item.Quantity = newQuantity;
    }
}