using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

namespace ZboxOrleans.Client;

public static class ClientHostBuilder
{
    public static IHostBuilder Create(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleansClient(clientBuilder =>
            {
                clientBuilder.Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "Cluster42";
                    options.ServiceId = "MyAwesomeService";
                });
                clientBuilder.UseLocalhostClustering();
                clientBuilder.UseTransactions();
                clientBuilder.Services.AddHostedService<MaxThroughputService>();
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseConsoleLifetime();
    }
}