namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Grain with persisted state
/// </summary>
public interface IUserPersistedGrain : IGrainWithGuidKey
{
    Task<string> GetUserNameAsync();
    Task SetUserNameAsync(string name);
}