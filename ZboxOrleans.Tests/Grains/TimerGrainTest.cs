using FluentAssertions;
using Xunit;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class TimerGrainTest : BaseGrainTest
{
    [Fact]
    public async Task TestTimerGrain()
    {
        var timer = await GetGrainAsync<ITimerGrain>(Guid.NewGuid());
        await timer.IncrementEvery(TimeSpan.FromMilliseconds(100));
        
        await Task.Delay(TimeSpan.FromMilliseconds(300));
        
        var counterValue = await timer.GetCounter();
        counterValue.Should().Be(4);
    }
    
    [Fact]
    public async Task TestTimerGrain_ShouldStopWhenDisposed()
    {
        var timer = await GetGrainAsync<ITimerGrain>(Guid.NewGuid());
        await timer.IncrementEvery(TimeSpan.FromMilliseconds(100));
        
        await Task.Delay(TimeSpan.FromMilliseconds(100));

        timer.Dispose();

        var counterValue = await timer.GetCounter();
        
        counterValue.Should().Be(2);
        
        await Task.Delay(TimeSpan.FromMilliseconds(200));
        
        var counterValueAfterDisposed = await timer.GetCounter();
        counterValueAfterDisposed.Should().Be(counterValue);
    }
}