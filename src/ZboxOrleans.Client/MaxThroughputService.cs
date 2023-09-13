using Microsoft.Extensions.Hosting;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Client;

// 11. Hostovaná služba, která volá 'bezstavový grain (stateless)': Vytvořte co-hostovanou službu
// v ramci stejného runtime za pomoci 'IHostedService', ve které budete maximalní možnou rychlostí
// volat bezstavový grain (stateless). Ověřte si počty volání za sekundu v Dashbordu.
public sealed class MaxThroughputService : IHostedService, IDisposable
{
    private readonly IClusterClient _clusterClient;
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public MaxThroughputService(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        DoInfiniteGrainMethodCalls(_cancellationTokenSource.Token);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }

    // 38k rps with dashboard's own web server
    // 430k rps with dashboard hosted in app
    private void DoInfiniteGrainMethodCalls(CancellationToken cancellationToken)
    {
        var primaryKey = Guid.Parse("454870c3-28c1-4023-ba44-2550ff9f6d0d");

        while (!cancellationToken.IsCancellationRequested)
        {
            Parallel.For(0, Environment.ProcessorCount, async _ =>
            {
                var grain = _clusterClient.GetGrain<IStatelessWorkerGrain>(primaryKey);
                await grain.GetActualDateTick();
            });
        }
    }
}