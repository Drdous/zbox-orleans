namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Grain using Orleans Reminder to set periodical reminder message
/// </summary>
/// Note for me: Reminders are more reliable because they are persisted and continue to trigger after partial/full cluster restarts
public interface IReminderGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Method to set reminder in seconds
    /// </summary>
    /// <param name="seconds">How often should be reminder invoked. Minimum is 60s</param>
    Task SetReminderAsync(int seconds);
    
    /// <summary>
    /// Method to return reminder message
    /// </summary>
    Task<string> GetReminderMessage();
    
    /// <summary>
    /// The number of times reminder was invoked
    /// </summary>
    /// <returns></returns>
    Task<int> GetReminderReceivedCount();

    /// <summary>
    /// Since reminders survive the lifetime of any single activation, they must be explicitly canceled (as opposed to being disposed)
    /// </summary>
    Task UnregisterReminder();
}