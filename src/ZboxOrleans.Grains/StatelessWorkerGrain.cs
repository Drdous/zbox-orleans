using Orleans.Concurrency;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 6. Bezstavový grain (stateless): Vytvořte bezstavový grain, který má maximální propustnost v možnostech volání metod grainu.

// Note for me: 'Stateless' doesn't mean grain cannot have a state.
// It's just not easy to coordinate state held by different activations
// because multiple activations of a stateless worker grain can be created on the same and different silos of the cluster
[StatelessWorker]
public sealed class StatelessWorkerGrain : Grain, IStatelessWorkerGrain
{
    public Task<long> GetActualDateTick()
    {
        return Task.FromResult(DateTime.UtcNow.Ticks);
    }
}