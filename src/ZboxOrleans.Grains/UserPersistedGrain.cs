using Orleans.Runtime;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;
using ZboxOrleans.Grains.States;

namespace ZboxOrleans.Grains;

// 4. Stavový grain (s persistencí): Implementujte stavový grain, který svůj stav persistuje. Můžete využít Azure CosmosDB nebo Azure Blob Storage pro persistenci stavu grainu.
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
    
    public Task<string> GetUserNameAsync() => Task.FromResult(_userProfileState.State.Name);

    public async Task SetUserNameAsync(string name)
    {
        _userProfileState.State.Name = name;
        await _userProfileState.WriteStateAsync();
    }
}