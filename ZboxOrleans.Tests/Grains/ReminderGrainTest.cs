using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class ReminderGrainTest : BaseGrainTest
{
    [Fact]
    public async Task TestReminderGrain()
    {
        var reminderGrain = await GetGrainAsync<IReminderGrain>(Guid.NewGuid());
        await reminderGrain.SetReminderAsync(60); // 1 minute is minimum for reminders
        
        (await reminderGrain.GetReminderReceivedCount()).Should().Be(0);
        (await reminderGrain.GetReminderMessage()).Should().Be("Go learn Orleans!");

        // Uncomment to test
        // await Task.Delay(TimeSpan.FromSeconds(61));
        // (await reminderGrain.GetReminderReceivedCount()).Should().Be(1);

        await reminderGrain.UnregisterReminder();
    }
}