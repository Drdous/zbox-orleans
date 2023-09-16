using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using ZboxOrleans.Core.Constants;

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
                    options.ClusterId = Constants.Cluster.ClusterId;
                    options.ServiceId = Constants.Cluster.ServiceId;
                });
                clientBuilder.UseLocalhostClustering();
                clientBuilder.UseTransactions();
                clientBuilder.Services.AddHostedService<MaxThroughputService>();
                clientBuilder.AddMemoryStreams(Constants.StreamProviders.StreamProvider);
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseConsoleLifetime();
    }
}