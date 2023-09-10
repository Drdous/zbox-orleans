namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Stateful persisted grain to perform consistent money transfer operations using orleans transactions.
/// </summary>
public interface IBankAccountGrain : IGrainWithGuidKey
{
    // Note for me: Join can be used under existing transaction only (ITransactionClient)
    [Transaction(TransactionOption.Join)]
    Task<bool> Withdraw(decimal amount);

    [Transaction(TransactionOption.Join)]
    Task Deposit(decimal amount);

    // Note for me: Not necessary to run under existing transaction. If not, it creates one.
    [Transaction(TransactionOption.CreateOrJoin)]
    Task<decimal> GetBalance();
}