namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Some demonstration of Reentrant grain where it would cause deadlock without reentrant attribute
/// </summary>
/// Note fore me: Reentrancy = Allow requests to be executed concurrently.
/// Can be done by marking class with [Reentrant] attribute, marking interface method by [AlwaysInterleave]
/// or grain's [MayInterleave] attribute's method predicate returns true.
public interface IReentrantGrain : IGrainWithGuidKey
{
    /// <summary>
    /// Method to get counter value
    /// </summary>
    Task<int> GetCounter();
    
    /// <summary>
    /// Method to increment counter in a reentrant manner
    /// </summary>
    Task IncrementCounter(int times);
    
    /// <summary>
    /// Recursive method A calling B to reach the depth
    /// </summary>
    Task StartRecursiveOperationA(int depth);
    
    /// <summary>
    /// Recursive method B calling A to reach the depth
    /// </summary>
    Task StartRecursiveOperationB(int depth);
}