namespace ZboxOrleans.Grains.States;

/// <summary>
/// User account state for persistence
/// </summary>
[Serializable]
public sealed class UserAccountState
{
    public string AccountId { get; set; } = null!;
}