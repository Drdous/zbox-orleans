namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// POCO Grain
/// Sources:
/// https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide#poco-grains-and-igrainbase
/// https://learn.microsoft.com/en-us/dotnet/standard/glossary#poco
/// </summary>
public interface IPocoGrain : IGrainWithGuidKey
{
    Task<int> AddNumbersAsync(int a, int b);
}