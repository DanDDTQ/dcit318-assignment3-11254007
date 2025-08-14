using System;

namespace FinanceManagementSystem;

public class FinanceApp
{
    private readonly List<Transaction> _transactions = new();

    public void Run()
    {
        // i. Instantiate a SavingsAccount with an account number and initial balance (e.g., 1000)
        var account = new SavingsAccount("SA-001-2025", 1000m);
        Console.WriteLine($"Created SavingsAccount {account.AccountNumber} with balance {account.Balance:C}\n");

        // ii. Create three Transaction records with sample values (Groceries, Utilities, Entertainment)
        var t1 = new Transaction(1, DateTime.UtcNow, 150.75m, "Groceries");
        var t2 = new Transaction(2, DateTime.UtcNow, 300.00m, "Utilities");
        var t3 = new Transaction(3, DateTime.UtcNow, 600.50m, "Entertainment");

        // iii. Use the processors
        ITransactionProcessor processor1 = new MobileMoneyProcessor();
        ITransactionProcessor processor2 = new BankTransferProcessor();
        ITransactionProcessor processor3 = new CryptoWalletProcessor();

        // Process each transaction (prints processing messages)
        processor1.Process(t1); // MobileMoneyProcessor → Transaction 1
        processor2.Process(t2); // BankTransferProcessor → Transaction 2
        processor3.Process(t3); // CryptoWalletProcessor → Transaction 3
        Console.WriteLine();

        // iv. Apply each transaction to the SavingsAccount using ApplyTransaction
        account.ApplyTransaction(t1); // should succeed
        account.ApplyTransaction(t2); // should succeed or fail depending on remaining balance
        account.ApplyTransaction(t3); // may print "Insufficient funds" if funds are low
        Console.WriteLine();

        // v. Add all transactions to _transactions
        _transactions.Add(t1);
        _transactions.Add(t2);
        _transactions.Add(t3);

        // Summary
        Console.WriteLine("Transaction log:");
        foreach (var tx in _transactions)
        {
            Console.WriteLine($" - Id: {tx.Id}, Date: {tx.Date:u}, Amount: {tx.Amount:C}, Category: {tx.Category}");
        }

        Console.WriteLine($"\nFinal balance for account {account.AccountNumber}: {account.Balance:C}");
    }
}