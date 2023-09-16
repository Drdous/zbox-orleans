namespace ZboxOrleans.GrainInterfaces;

public interface IStreamProducerGrain : IGrainWithGuidKey
{
    Task ProduceMessagesAsync();
}