namespace ZboxOrleans.GrainInterfaces;

public interface IUserAccountGrain : IGrainWithGuidKey
{
    Task<string> GetAccountIdAsync();
    Task SetAccountIdAsync(string accountId);
}