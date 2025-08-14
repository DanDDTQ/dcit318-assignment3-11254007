using System;

namespace FinanceManagementSystem;

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        if (transaction.Amount <= 0)
        {
            Console.WriteLine($"[CryptoWallet] Invalid amount ({transaction.Amount}). Skipping processing.");
            return;
        }

        Console.WriteLine($"[CryptoWallet] Processing crypto wallet transfer. Amount: {transaction.Amount:C}, Category: {transaction.Category}");
        
    }
}
