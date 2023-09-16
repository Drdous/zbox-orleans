using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class AggregatorTest : BaseGrainTest
{
    [Fact]
    public async Task TestAggregatorGrain_ShouldAggregateGrainValue()
    {
        var aggregatorGrain = await GetGrainAsync<IAggregatorGrain>(Guid.NewGuid());

        // Simulate multiple sensors
        for (int i = 1; i <= 5; i++)
        {
            var sensorGrain = await GetGrainAsync<ITemperatureSensorGrain>(Guid.NewGuid());
            await aggregatorGrain.AddSensor(sensorGrain);
        }

        var result = await aggregatorGrain.CalculateAverage();
        result.Should().BeGreaterThan(0);
    }
}