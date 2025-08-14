using System;

namespace WarehouseInventorySystem;
public class GroceryItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; }

    public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (expiryDate == default) throw new ArgumentException("Expiry date required", nameof(expiryDate));

        Id = id;
        Name = name;
        Quantity = quantity;
        ExpiryDate = expiryDate;
    }

    public override string ToString()
        => $"GroceryItem(Id={Id}, Name={Name}, Qty={Quantity}, Expiry={ExpiryDate:yyyy-MM-dd})";
}
