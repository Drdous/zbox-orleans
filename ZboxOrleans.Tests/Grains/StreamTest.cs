using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class StreamTest : BaseGrainTest
{
    // Investigate why the silo is blocking application messages
    [Fact]
    public async Task TestStreamsForDebugPurposes()
    {
        var producer = await GetGrainAsync<IStreamProducerGrain>(Guid.NewGuid());

        await producer.ProduceMessagesAsync();
    }
}