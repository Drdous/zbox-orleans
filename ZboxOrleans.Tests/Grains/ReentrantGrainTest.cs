using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class ReentrantGrainTest : BaseGrainTest
{
    [Fact]
    public async Task TestReentrantGrain_ShouldRecursivelyCallOtherMethodWithoutDeadlock()
    {
        var reentrantGrain = await GetGrainAsync<IReentrantGrain>(Guid.NewGuid());
        await reentrantGrain.StartRecursiveOperationA(10);
    }
    
    [Fact]
    public async Task TestReentrantGrain_ShouldRecursivelyCallItself()
    {
        var reentrantGrain = await GetGrainAsync<IReentrantGrain>(Guid.NewGuid());
        await reentrantGrain.IncrementCounter(10);
        (await reentrantGrain.GetCounter()).Should().Be(10);
    }
}