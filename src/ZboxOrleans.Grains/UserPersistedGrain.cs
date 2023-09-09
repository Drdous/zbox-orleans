using Orleans.Runtime;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;
using ZboxOrleans.Grains.States;

namespace ZboxOrleans.Grains;

public sealed class UserPersistedGrain : Grain, IUserPersistedGrain
{
    private readonly IPersistentState<UserProfileState> _userProfileState;

    // Note for me: State is not loaded when injected. It's loaded before OnActivateAsync() is called.
    public UserPersistedGrain(
        [PersistentState(PersistenceConstants.StateNames.UserProfile, PersistenceConstants.ProviderNames.AzureBlobStorage)]
        IPersistentState<UserProfileState> userProfileState)
    {
        _userProfileState = userProfileState;
        
    }
    
    public Task<string> GetNameAsync() => Task.FromResult(_userProfileState.State.Name);

    public async Task SetNameAsync(string name)
    {
        _userProfileState.State.Name = name;
        await _userProfileState.WriteStateAsync();
    }
}