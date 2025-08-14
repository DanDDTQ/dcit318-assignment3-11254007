using System;

namespace FinanceManagementSystem;
public interface ITransactionProcessor
{
    void Process(Transaction transaction);
}
