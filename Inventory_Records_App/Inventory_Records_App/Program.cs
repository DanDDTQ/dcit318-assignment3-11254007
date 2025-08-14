using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace InventoryRecordsApp;

    
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity
    {
        // Make a nicer string for display
        public override string ToString() =>
            $"{Id}: {Name} (Qty: {Quantity}) Added: {DateAdded:yyyy-MM-dd}";
    }

   
    public static class Program
    {
        public static void Main()
        {
            // file path next to the running exe
            string exeFolder = AppContext.BaseDirectory;
            string fileName = "inventory.json";
            string filePath = Path.Combine(exeFolder, fileName);

            Console.WriteLine("Executable folder: " + exeFolder);
            Console.WriteLine("Data file: " + filePath);
            Console.WriteLine();

            var app = new InventoryApp(filePath);

            // 1) Seed
            app.SeedSampleData();

            // 2) Save to disk
            try
            {
                app.SaveData();
            }
            catch
            {
                Console.WriteLine("Failed to save data. Aborting.");
                return;
            }

            // 3) Clear memory and simulate new session
            app.ClearMemory();
            Console.WriteLine("\nCleared in-memory data to simulate a new session.\n");

            // 4) Load from disk
            try
            {
                app.LoadData();
            }
            catch
            {
                Console.WriteLine("Failed to load data. Aborting.");
                return;
            }

            // 5) Print to confirm recovery
            Console.WriteLine();
            app.PrintAllItems();

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }

