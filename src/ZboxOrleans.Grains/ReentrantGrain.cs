using Orleans.Concurrency;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 9. Reentrant Grains: Vytvořte grain, který demonstruje použití vlatnosti reentrant v Orleans.
[Reentrant]
public sealed class ReentrantGrain : Grain, IReentrantGrain
{
    private int _counter = 0;

    public Task<int> GetCounter()
    {
        return Task.FromResult(_counter);
    }

    public async Task IncrementCounter(int times)
    {
        if (times > 0)
        {
            await IncrementCounter(times - 1);
            _counter++;
        }
    }
    
    public async Task StartRecursiveOperationA(int depth)
    {
        if (depth is 0)
        {
            return;
        }

        await StartRecursiveOperationB(depth - 1);
    }
    
    public async Task StartRecursiveOperationB(int depth)
    {
        if (depth is 0)
        {
            return;
        }

        await StartRecursiveOperationA(depth - 1);
    }
}