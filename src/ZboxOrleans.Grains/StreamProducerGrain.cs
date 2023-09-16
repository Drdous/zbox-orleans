using Orleans.Runtime;
using Orleans.Streams;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

// 13. Virtuální streamy: Implementujte virtuální streamy v Orleans pro asynchronní komunikaci mezi grainy.
public sealed class StreamProducerGrain : Grain, IStreamProducerGrain
{
    private readonly IClusterClient _clusterClient;
    private IAsyncStream<int> _stream;

    public StreamProducerGrain(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var streamId = StreamId.Create(Constants.StreamNamespaces.Namespace1, Guid.Parse("cd85e067-ca2e-43cb-8094-8e1fecfd9197"));
        _stream = _clusterClient.GetStreamProvider(Constants.StreamProviders.StreamProvider).GetStream<int>(streamId);

        return base.OnActivateAsync(cancellationToken);
    }

    public async Task ProduceMessagesAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            await _stream.OnNextAsync(i);
            Console.WriteLine($"Produced message: {i}");
        }
    }
}