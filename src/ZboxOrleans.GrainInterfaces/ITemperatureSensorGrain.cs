namespace ZboxOrleans.GrainInterfaces;

/// <summary>
/// Used to demonstrate aggregation pattern
/// </summary>
public interface ITemperatureSensorGrain : IGrainWithGuidKey
{
    Task<int> GetSensorValue();
}