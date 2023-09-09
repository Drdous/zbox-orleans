using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Hosting;

namespace ZboxOrleans.Client;

public static class ClientHostBuilder
{
    public static IHostBuilder Create(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleansClient(clientBuilder =>
            {
                clientBuilder.UseLocalhostClustering();
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseConsoleLifetime();
    }
}