namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Grain using timer to increment integer according to the set interval
/// </summary>
public interface ITimerGrain : IGrainWithGuidKey, IDisposable
{
    /// <summary>
    /// Method which set timer to the specified period
    /// </summary>
    /// <param name="period">How often to increment</param>
    Task IncrementEvery(TimeSpan period);
    
    /// <summary>
    /// Get actual timer value
    /// </summary>
    /// <returns>Integer timer value</returns>
    Task<int> GetCounter();
}