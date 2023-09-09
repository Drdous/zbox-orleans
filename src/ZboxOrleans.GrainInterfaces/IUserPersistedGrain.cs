namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Grain with persisted state
/// </summary>
public interface IUserPersistedGrain : IGrainWithGuidKey
{
    Task<string> GetNameAsync();
    Task SetNameAsync(string name);
}