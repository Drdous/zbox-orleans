using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class BankAccountTransactionsTest : BaseGrainTest
{
    [Fact]
    public async Task TestTransactionsConsistency()
    {
        var accounts = new List<(Guid AccountId, string UserName)>
        {
            ( Guid.Parse("454870c3-28c1-4023-ba44-2550ff9f6d0d"), "Tony Stark" ), 
            ( Guid.Parse("19a5ebd7-c6e1-499a-8451-5bce0edec027"), "Peter Griffin" ),
            ( Guid.Parse("8956206e-9777-4c29-bfdf-5dd90b8b5463"), "Apu Nahasapeemapetilon" ),
            ( Guid.Parse("cd85e067-ca2e-43cb-8094-8e1fecfd9197"), "Bill Gates" ),
            ( Guid.Parse("8cbc15d0-c751-4bc6-9559-96a5ff94f41d"), "Angelina Jolie" ),
        };
        
        await InitializeIfNotExist();
        var transactionClient = GetService<ITransactionClient>();

        var random = Random.Shared;

        Parallel.For(0, 100, async i =>
        {
            var senderIndex = random.Next(accounts.Count);
            var recipientIndex = random.Next(accounts.Count);
            while (recipientIndex == senderIndex)
            {
                recipientIndex = (recipientIndex + 1) % accounts.Count;
            }

            var senderAccount = accounts[senderIndex];
            var recipientAccount = accounts[recipientIndex];

            var senderAccountGrain = GrainFactory!.GetGrain<IBankAccountGrain>(senderAccount.AccountId);
            var recipientAccountGrain = GrainFactory.GetGrain<IBankAccountGrain>(recipientAccount.AccountId);

            var senderBalanceBeforeTransfer = await senderAccountGrain.GetBalance();
            var recipientBalanceBeforeTransfer = await recipientAccountGrain.GetBalance();

            var transferAmount = random.Next(1, 200);

            await transactionClient.RunTransaction(
                TransactionOption.Create,
                async () =>
                {
                    if (await senderAccountGrain.Withdraw(transferAmount))
                    {
                        await recipientAccountGrain.Deposit(transferAmount);
                    }
                });

            var senderBalanceAfterTransfer = await senderAccountGrain.GetBalance();
            var recipientBalanceAfterTransfer = await recipientAccountGrain.GetBalance();

            AssertBalanceIsConsistent(senderBalanceBeforeTransfer, recipientBalanceBeforeTransfer,
                senderBalanceAfterTransfer, recipientBalanceAfterTransfer, transferAmount);
        });
    }

    private void AssertBalanceIsConsistent(decimal senderBalanceBeforeTransfer, decimal recipientBalanceBeforeTransfer, decimal senderBalanceAfterTransfer, decimal recipientBalanceAfterTransfer, int transferAmount)
    {
        if (senderBalanceBeforeTransfer < transferAmount)
        {
            senderBalanceAfterTransfer.Should().Be(senderBalanceBeforeTransfer);
            recipientBalanceAfterTransfer.Should().Be(recipientBalanceBeforeTransfer);
            return;
        }

        senderBalanceAfterTransfer.Should().Be(senderBalanceBeforeTransfer - transferAmount);
        recipientBalanceAfterTransfer.Should().Be(recipientBalanceBeforeTransfer + transferAmount);
    }
}