using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

public sealed class NotPersistedStatefulGrain : Grain, INotPersistedStatefulGrain
{
    private int _counter = 0;
    
    public Task<int> GetCounterAsync()
    {
        return Task.FromResult(_counter);
    }

    public Task IncrementCounterAsync()
    {
        _counter++;
        return Task.CompletedTask;
    }
}
