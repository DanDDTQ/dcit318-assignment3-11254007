using System;

namespace WarehouseInventorySystem;
public class WareHouseManager
{
    // Exposed so Main() can pass the repositories to generic methods (alternatively could provide wrapper methods)
    public InventoryRepository<ElectronicItem> _electronics { get; } = new();
    public InventoryRepository<GroceryItem> _groceries { get; } = new();

    // Seed 2-3 items of each type (handles duplicate errors gracefully)
    public void SeedData()
    {
        Console.WriteLine("Seeding data...\n");

        // Electronics
        AddItem(_electronics, new ElectronicItem(1, "Laptop", 10, "Dell", 24));
        AddItem(_electronics, new ElectronicItem(2, "Smartphone", 25, "Samsung", 12));
        AddItem(_electronics, new ElectronicItem(3, "Router", 5, "TP-Link", 18));

        // Groceries
        AddItem(_groceries, new GroceryItem(101, "Rice (5kg)", 50, DateTime.UtcNow.AddMonths(12)));
        AddItem(_groceries, new GroceryItem(102, "Milk (1L)", 30, DateTime.UtcNow.AddDays(14)));
        AddItem(_groceries, new GroceryItem(103, "Eggs (dozen)", 20, DateTime.UtcNow.AddDays(21)));

        Console.WriteLine();
    }

    // Generic print method
    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        var items = repo.GetAllItems();
        Console.WriteLine($"Printing all {typeof(T).Name} items ({items.Count}):");
        foreach (var item in items)
        {
            Console.WriteLine(" - " + item);
        }
        Console.WriteLine();
    }

    // Add any item; catches DuplicateItemException and prints friendly message
    public void AddItem<T>(InventoryRepository<T> repo, T item) where T : IInventoryItem
    {
        try
        {
            repo.AddItem(item);
            Console.WriteLine($"Added item: {item}");
        }
        catch (DuplicateItemException dex)
        {
            Console.WriteLine($"[Error - Duplicate] {dex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error - AddItem] {ex.Message}");
        }
    }

    // Increase stock by quantity (validates item existence and non-negative computed quantity)
    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantityToAdd) where T : IInventoryItem
    {
        try
        {
            if (quantityToAdd < 0)
                throw new InvalidQuantityException("Quantity to add must be non-negative.");

            var item = repo.GetItemById(id);
            int newQty = item.Quantity + quantityToAdd;
            repo.UpdateQuantity(id, newQty);
            Console.WriteLine($"Stock increased for ID {id}. New quantity: {newQty}");
        }
        catch (ItemNotFoundException inf)
        {
            Console.WriteLine($"[Error - Not Found] {inf.Message}");
        }
        catch (InvalidQuantityException iq)
        {
            Console.WriteLine($"[Error - Invalid Quantity] {iq.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error - IncreaseStock] {ex.Message}");
        }
    }

    // Remove an item by ID with friendly error handling
    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Removed item with ID {id}");
        }
        catch (ItemNotFoundException inf)
        {
            Console.WriteLine($"[Error - Not Found] {inf.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Error - RemoveItem] {ex.Message}");
        }
    }
}
