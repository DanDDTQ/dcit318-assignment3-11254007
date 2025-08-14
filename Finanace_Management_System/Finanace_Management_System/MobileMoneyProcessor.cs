using System;

namespace FinanceManagementSystem;

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            Console.WriteLine($"[MobileMoney] Invalid amount ({transaction.Amount}). Skipping processing.");
            return;
        }

        Console.WriteLine($"[MobileMoney] Processing mobile money payment. Amount: {transaction.Amount:C}, Category: {transaction.Category}");
        // Additional domain logic could go here
    }
}
