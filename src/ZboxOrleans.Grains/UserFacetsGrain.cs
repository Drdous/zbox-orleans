using Orleans.Runtime;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;
using ZboxOrleans.Grains.States;

namespace ZboxOrleans.Grains;

// 5. Více stavů v jednom grainu (přes facets): Demonstrujte, jak může grain obsahovat více stavů pomocí Orleans grain facets.
public sealed class UserFacetsGrain : Grain, IUserPersistedGrain, IUserAccountGrain
{
    private readonly IPersistentState<UserProfileState> _userProfileState;
    private readonly IPersistentState<UserAccountState> _userAccountState;

    public UserFacetsGrain(
        [PersistentState(Constants.StateNames.UserProfile, Constants.ProviderNames.AzureBlobStorage)]
        IPersistentState<UserProfileState> userProfileState,
        [PersistentState(Constants.StateNames.UserProfile, Constants.ProviderNames.AzureBlobStorage)]
        IPersistentState<UserAccountState> userAccountState)
    {
        _userProfileState = userProfileState;
        _userAccountState = userAccountState;
    }

    #region UserProfile

    public Task<string> GetUserNameAsync() => Task.FromResult(_userProfileState.State.Name);

    public async Task SetUserNameAsync(string name)
    {
        _userProfileState.State.Name = name;
        await _userProfileState.WriteStateAsync();
    }

    #endregion

    #region UserAccount

    public Task<string> GetAccountIdAsync() => Task.FromResult(_userAccountState.State.AccountId);

    public async Task SetAccountIdAsync(string accountId)
    {
        _userAccountState.State.AccountId = accountId;
        await _userAccountState.WriteStateAsync();
    }

    #endregion
}