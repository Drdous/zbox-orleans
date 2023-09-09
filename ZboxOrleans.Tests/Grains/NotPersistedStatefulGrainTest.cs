using FluentAssertions;
using ZboxOrleans.GrainInterfaces;
using Xunit;

namespace ZboxOrleans.Tests.Grains;

public sealed class NotPersistedStatefulGrainTest : BaseGrainTest
{
    [Fact]
    public async Task TestNotPersistedStatefulGrain_ShouldKeepState()
    {
        var primaryKeyGuid = Guid.NewGuid();
        var grain = await GetGrainAsync<INotPersistedStatefulGrain>(primaryKeyGuid);

        await grain.IncrementCounterAsync();
        
        (await grain.GetCounterAsync()).Should().Be(1);
        
        var grain2 = await GetGrainAsync<INotPersistedStatefulGrain>(primaryKeyGuid);
        (await grain2.GetCounterAsync()).Should().Be(1);
    }
}