using System;
using System.Collections.Generic;

namespace FinanceManagementSystem
{   // Core model: immutable record for a transaction
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);



    // Main application orchestration


    class Program
    {
        static void Main()
        {
            var app = new FinanceApp();
            app.Run();

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}

