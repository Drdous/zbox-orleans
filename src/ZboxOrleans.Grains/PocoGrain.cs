using Orleans.Runtime;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

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