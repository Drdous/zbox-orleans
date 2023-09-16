using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZboxOrleans.GrainInterfaces;
using ZboxOrleans.Silo;

using var host = SiloHostBuilder.Create(args).Build();

// await TryStreamForDebugPurposes();

await host.RunAsync();
return;

// TODO: Investigate why the silo is blocking application messages when used from Test and move to StreamTest
async Task TryStreamForDebugPurposes()
{
    await host.StartAsync();

    IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

    var producer = client.GetGrain<IStreamProducerGrain>(Guid.NewGuid());
    await producer.ProduceMessagesAsync();

    Console.ReadKey();
}

