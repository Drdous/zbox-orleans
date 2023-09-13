using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZboxOrleans.Client;

public static class ClientHostBuilder
{
    public static IHostBuilder Create(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleansClient(clientBuilder =>
            {
                clientBuilder.UseLocalhostClustering();
                clientBuilder.UseTransactions();
                clientBuilder.Services.AddHostedService<MaxThroughputService>();
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseConsoleLifetime();
    }
}