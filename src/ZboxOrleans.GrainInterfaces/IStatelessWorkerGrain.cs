namespace ZboxOrleans.GrainInterfaces;

public interface IStatelessWorkerGrain : IGrainWithGuidKey
{
    Task<int> GetRandomIntAsync();
}