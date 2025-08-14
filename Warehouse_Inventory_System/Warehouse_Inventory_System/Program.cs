using System;
using System.Collections.Generic;

namespace WarehouseInventorySystem
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    

    internal class Program
    {
        private static void Main()
        {
            var manager = new WareHouseManager();

            // i. SeedData
            manager.SeedData();

            // ii & iii. Print all grocery items
            manager.PrintAllItems(manager._groceries);

            // iv. Print all electronic items
            manager.PrintAllItems(manager._electronics);

            // v. Try operations that cause exceptions

            Console.WriteLine("== Attempt to add a duplicate electronic item ==");
            // Attempt to add duplicate (ID 1 already present)
            var duplicateElectronic = new ElectronicItem(1, "Laptop Pro", 5, "Dell", 36);
            manager.AddItem(manager._electronics, duplicateElectronic);
            Console.WriteLine();

            Console.WriteLine("== Attempt to remove a non-existent grocery item ==");
            // Remove a non-existing ID (e.g., 999)
            manager.RemoveItemById(manager._groceries, 999);
            Console.WriteLine();

            Console.WriteLine("== Attempt to update with invalid quantity ==");
            // Attempt to set invalid negative quantity using IncreaseStock with negative add
            // We'll call IncreaseStock with negative to trigger InvalidQuantityException in method
            manager.IncreaseStock(manager._electronics, 2, -5);

            // Additionally, try repo.UpdateQuantity directly (simulate invalid update)
            Console.WriteLine("\n== Attempt direct invalid UpdateQuantity (should be caught) ==");
            try
            {
                manager._electronics.UpdateQuantity(2, -10); // negative -> InvalidQuantityException
            }
            catch (InvalidQuantityException iq)
            {
                Console.WriteLine($"[Caught in Main] {iq.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Caught in Main] Unexpected: {ex.Message}");
            }

            Console.WriteLine("\nFinal state of inventories:");
            manager.PrintAllItems(manager._electronics);
            manager.PrintAllItems(manager._groceries);

            
        }
       
    }
    
}
