using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 8. Timers and Reminders: Vytvořte příklad grainu, který ukazuje, jak používat timers a reminders v Orleans.
public sealed class TimerGrain : Grain, ITimerGrain
{
    private IDisposable? _timer;
    private int _counter;
    
    // This method will be called each time the timer fires.
    private Task TimerCallback(object state)
    {
        _counter++;
        return Task.CompletedTask;
    }
    
    public Task IncrementEvery(TimeSpan period)
    {
        var dueTime = TimeSpan.Zero; // Timer starts immediately.
        _timer = RegisterTimer(TimerCallback, null, dueTime, period);
        return Task.CompletedTask;
    }

    public Task<int> GetCounter()
    {
        return Task.FromResult(_counter);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        Dispose();
        return base.OnDeactivateAsync(reason, cancellationToken);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}