using System;

namespace FinanceManagementSystem;

public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    // Override to enforce no overdrafts
    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction is null)
            throw new ArgumentNullException(nameof(transaction));

        if (transaction.Amount <= 0)
        {
            Console.WriteLine("[SavingsAccount] Transaction amount must be greater than zero. Transaction ignored.");
            return;
        }

        if (transaction.Amount > Balance)
        {
            Console.WriteLine("[SavingsAccount] Insufficient funds");
            return;
        }

        Balance -= transaction.Amount;
        Console.WriteLine($"[SavingsAccount] Transaction {transaction.Id} applied. Updated balance: {Balance:C}");
    }
}

