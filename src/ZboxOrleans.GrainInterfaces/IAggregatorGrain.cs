namespace ZboxOrleans.GrainInterfaces;

public interface IAggregatorGrain : IGrainWithGuidKey
{
    Task AddSensor(ITemperatureSensorGrain temperatureSensor);
    Task<double> CalculateAverage();
}