using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class UserPersistedGrainTest : BaseGrainTest
{
    [Fact]
    public async Task TestPersistedGrain_ShouldSaveState()
    {
        var name = "Peter Griffin";
        var primaryKeyGuid = Guid.Parse("9f7ef245-134d-42ba-a7b1-77e144a99fc5");
        var grain = await GetGrainAsync<IUserPersistedGrain>(primaryKeyGuid);

        await grain.SetNameAsync(name);
        (await grain.GetNameAsync()).Should().Be(name);
    }
}