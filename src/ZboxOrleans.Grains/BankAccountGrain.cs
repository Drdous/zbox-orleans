using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;
using ZboxOrleans.Grains.States;

namespace ZboxOrleans.Grains;

// 7. Distributed Transactions: Vytvořte přiklad grainů, které ukazují, jak v Orleans provádět distribuované transakce.
// Mohlo by to být například jednoduché bankovní rozhraní, kde můžete převádět peníze mezi účty a musíte se ujistit, že transakce jsou konzistentní.
[Reentrant]
public sealed class BankAccountGrain : Grain, IBankAccountGrain
{
    private readonly ITransactionalState<BalanceState> _balance;

    public BankAccountGrain(
        [TransactionalState(Constants.StateNames.Balance, Constants.ProviderNames.AzureBlobStorage)]
        ITransactionalState<BalanceState> balance)
    {
        _balance = balance;
    }
    
    public Task<bool> Withdraw(decimal amount) =>
        _balance.PerformUpdate(balance =>
        {
            if (balance.Value < amount)
            {
                return false;
            }

            balance.Value -= amount;
            return true;
        });

    public Task Deposit(decimal amount) =>
        _balance.PerformUpdate(balance => balance.Value += amount);
    

    public Task<decimal> GetBalance() =>
        _balance.PerformRead(balance => balance.Value);
}