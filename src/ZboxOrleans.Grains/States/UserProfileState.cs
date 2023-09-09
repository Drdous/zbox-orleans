namespace ZboxOrleans.Grains.States;

/// <summary>
/// State for persistence
/// </summary>
[Serializable]
public sealed class UserProfileState
{
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}