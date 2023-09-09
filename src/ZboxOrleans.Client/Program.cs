using Microsoft.Extensions.DependencyInjection;
using Orleans;
using ZboxOrleans.Client;

using var host = ClientHostBuilder.Create(args).Build();
await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

Console.ReadKey();

await host.StopAsync();