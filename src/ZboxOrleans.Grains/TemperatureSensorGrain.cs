using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

public sealed class TemperatureSensorGrain : Grain, ITemperatureSensorGrain
{
    private readonly Random _random = new();

    public Task<int> GetSensorValue()
    {
        // Simulate sensor data
        return Task.FromResult(_random.Next(1, 101));
    }
}