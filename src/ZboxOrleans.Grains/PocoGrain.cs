using Orleans.Runtime;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 2. POCO Grain: Vytvořte jednoduchý POCO grain, který neuchovává stav mezi voláními. POCO grainy v Orleans 7 nevyžadují dědění od třídy Grain.
public sealed class PocoGrain : IGrainBase, IPocoGrain
{
    public IGrainContext GrainContext { get; }
    
    public PocoGrain(IGrainContext context)
    {
        GrainContext = context;
    }
    
    public Task<int> AddNumbersAsync(int a, int b)
    {
        return Task.FromResult(a + b);
    }
}