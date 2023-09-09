using Microsoft.Extensions.Hosting;
using ZboxOrleans.Silo;

using var host = SiloHostBuilder.Create(args).Build();

await host.RunAsync();