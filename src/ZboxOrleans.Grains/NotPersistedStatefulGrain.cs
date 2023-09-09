using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 3. Stavový grain (bez persistence): Vytvořte stavový grain, který udržuje stav mezi voláními, ale není persistován.
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
