using System;

namespace InventoryRecordsApp;
public class InventoryApp
{
    private readonly InventoryLogger<InventoryItem> _logger;

    public InventoryApp(string filePath)
    {
        _logger = new InventoryLogger<InventoryItem>(filePath);
    }

    public void SeedSampleData()
    {
        // Create a few InventoryItem records (immutable)
        var now = DateTime.UtcNow.Date;
        try
        {
            _logger.Add(new InventoryItem(1, "USB-C Cable", 25, now.AddDays(-10)));
            _logger.Add(new InventoryItem(2, "Wireless Mouse", 15, now.AddDays(-5)));
            _logger.Add(new InventoryItem(3, "Keyboard - Mechanical", 8, now.AddDays(-2)));
            _logger.Add(new InventoryItem(4, "27\" Monitor", 6, now.AddDays(-1)));
            _logger.Add(new InventoryItem(5, "Laptop Stand", 12, now));
        }
        catch (InvalidOperationException inv)
        {
            Console.WriteLine($"Warning when seeding: {inv.Message}");
        }
    }

    public void SaveData()
    {
        Console.WriteLine("Saving data to disk...");
        _logger.SaveToFile();
        Console.WriteLine("Save complete.");
    }

    public void LoadData()
    {
        Console.WriteLine("Loading data from disk...");
        _logger.LoadFromFile();
        Console.WriteLine("Load complete.");
    }

    public void PrintAllItems()
    {
        var items = _logger.GetAll();
        if (items.Count == 0)
        {
            Console.WriteLine("(no items)");
            return;
        }

        Console.WriteLine("Inventory items:");
        foreach (var it in items)
        {
            Console.WriteLine(" - " + it);
        }
    }

    // Expose a way to clear in-memory log (simulate restart)
    public void ClearMemory() => _logger.Clear();
}

