namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Stateful grain preserving counter state
/// </summary>
public interface INotPersistedStatefulGrain : IGrainWithGuidKey
{
    Task<int> GetCounterAsync();
    Task IncrementCounterAsync();
}