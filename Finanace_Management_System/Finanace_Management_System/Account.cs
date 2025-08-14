using System;

namespace FinanceManagementSystem;

public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number must be provided.", nameof(accountNumber));
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));

        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    // Virtual by default: deducts the amount (no overdraft checks here)
    public virtual void ApplyTransaction(Transaction transaction)
    {
        if (transaction is null)
            throw new ArgumentNullException(nameof(transaction));

        if (transaction.Amount <= 0)
        {
            Console.WriteLine("Transaction amount must be greater than zero. Transaction ignored.");
            return;
        }

        Balance -= transaction.Amount;
        Console.WriteLine($"[Account] Applied transaction {transaction.Id}. New balance: {Balance:C}");
    }
}

