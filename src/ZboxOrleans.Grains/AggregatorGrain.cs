using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 15. Agregační vzor: Demonstrujte, jak lze v Orleans implementovat agregační vzor.
public sealed class AggregatorGrain : Grain, IAggregatorGrain
{
    private readonly List<ITemperatureSensorGrain> _sensors = new();

    public Task AddSensor(ITemperatureSensorGrain temperatureSensor)
    {
        _sensors.Add(temperatureSensor);
        return Task.CompletedTask;
    }

    public async Task<double> CalculateAverage()
    {
        if (_sensors.Count == 0)
            return 0;

        double total = 0;
        foreach (var sensor in _sensors)
        {
            total += await sensor.GetSensorValue();
        }

        return total / _sensors.Count;
    }
}