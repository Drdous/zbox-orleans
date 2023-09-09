using FluentAssertions;
using ZboxOrleans.GrainInterfaces;
using Xunit;

namespace ZboxOrleans.Tests.Grains;

public sealed class PocoGrainTest : BaseGrainTest
{
    private readonly Guid _primaryKeyGuid = Guid.NewGuid();
    
    [Fact]
    public async Task TestPocoGrainCreate_WithValidGuid_ShouldReturnGuidAsPrimaryKey()
    {
        var grain = await GetGrainAsync<IPocoGrain>(_primaryKeyGuid);
        grain.GetPrimaryKey().Should().Be(_primaryKeyGuid);
    }

    [Fact]
    public async Task TestPocoGrainMethod_ShouldReturnCorrectResult()
    {
        var grain = await GetGrainAsync<IPocoGrain>(_primaryKeyGuid);
        (await grain.AddNumbersAsync(2, 3)).Should().Be(5);
    }
}