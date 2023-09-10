using Orleans.Concurrency;

namespace ZboxOrleans.GrainInterfaces;

public interface IStatelessWorkerGrain : IGrainWithGuidKey
{
    // Note for me: ReadOnly attribute can improve performance because allows to do it concurrently (it's safe because nothing is modified inside)
    [ReadOnly]
    Task<long> GetActualDateTick();
}