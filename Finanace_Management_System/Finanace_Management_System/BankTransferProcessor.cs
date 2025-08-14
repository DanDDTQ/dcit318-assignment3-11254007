using System;

namespace FinanceManagementSystem;


public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            Console.WriteLine($"[BankTransfer] Invalid amount ({transaction.Amount}). Skipping processing.");
            return;
        }

        Console.WriteLine($"[BankTransfer] Processing bank transfer. Amount: {transaction.Amount:C}, Category: {transaction.Category}");
        // Additional domain logic could go here (logging, validation, settlement, etc.)
    }
}
