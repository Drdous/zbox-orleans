using ZboxOrleans.Client;

using var host = ClientHostBuilder.Create(args).Build();
await host.StartAsync();

Console.ReadKey();

await host.StopAsync();