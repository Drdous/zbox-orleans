using Orleans.Runtime;
using Orleans.Streams;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Grains;

[ImplicitStreamSubscription(Constants.StreamNamespaces.Namespace1)]
public sealed class StreamConsumerGrain : Grain, IStreamConsumerGrain
{
    private readonly IClusterClient _clusterClient;

    public StreamConsumerGrain(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }
    
    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var streamId = StreamId.Create(Constants.StreamNamespaces.Namespace1, this.GetPrimaryKey());
        var stream = _clusterClient.GetStreamProvider(Constants.StreamProviders.StreamProvider).GetStream<int>(streamId);

        // Explicit subscription
        
        // var subscriptionHandles = (await stream.GetAllSubscriptionHandles()).ToList();
        //
        // subscriptionHandles.ForEach(
        //     async x => await x.ResumeAsync(async (data, _) =>
        //     {
        //         Console.WriteLine(data);
        //         await Task.CompletedTask;
        //     }));
        
        
        // 14. Pub/Sub vzor: Využijte Orleans pro implementaci publish/subscribe vzoru pro asynchronní zpracování událostí.
        // Implicit subscription
        await stream.SubscribeAsync(
            async (data, _) =>
            {
                Console.WriteLine(data);
                await Task.CompletedTask;
            });
        
        await base.OnActivateAsync(cancellationToken);
    }
}