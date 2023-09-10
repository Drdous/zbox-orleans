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
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseConsoleLifetime();
    }
}