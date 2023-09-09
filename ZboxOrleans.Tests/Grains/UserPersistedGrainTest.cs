using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

[TestCaseOrderer(ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer", ordererAssemblyName: "XUnit.Project")]
public sealed class UserPersistedGrainTest : BaseGrainTest
{
    private const string TestName = "Peter Griffin";
    private readonly Guid _primaryKeyGuid = Guid.Parse("9f7ef245-134d-42ba-a7b1-77e144a99fc5");
    
    [Fact]
    public async Task TestA_PersistedGrain_ShouldSaveState()
    {
        var grain = await GetGrainAsync<IUserPersistedGrain>(_primaryKeyGuid);

        await grain.SetUserNameAsync(TestName);
        (await grain.GetUserNameAsync()).Should().Be(TestName);
    }

    [Fact]
    public async Task TestB_PersistedGrain_ShouldLoadState()
    {
        var grain = await GetGrainAsync<IUserPersistedGrain>(_primaryKeyGuid);

        (await grain.GetUserNameAsync()).Should().Be(TestName);
    }
}