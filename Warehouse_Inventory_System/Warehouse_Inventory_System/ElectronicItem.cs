using System;

namespace WarehouseInventorySystem;
public class ElectronicItem : IInventoryItem
{
    public int Id { get; }
    public string Name { get; }
    public int Quantity { get; set; }
    public string Brand { get; }
    public int WarrantyMonths { get; }

    public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentException("Brand required", nameof(brand));
        if (warrantyMonths < 0) throw new ArgumentOutOfRangeException(nameof(warrantyMonths));

        Id = id;
        Name = name;
        Quantity = quantity;
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public override string ToString()
        => $"ElectronicItem(Id={Id}, Name={Name}, Qty={Quantity}, Brand={Brand}, Warranty={WarrantyMonths}mo)";
}