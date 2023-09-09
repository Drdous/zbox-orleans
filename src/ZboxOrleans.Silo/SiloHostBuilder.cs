using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZboxOrleans.Silo;

public static class SiloHostBuilder
{
    public static IHostBuilder Create(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleans(silo =>
            {
                silo.UseLocalhostClustering()
                    .ConfigureLogging(logging => logging.AddConsole());
            })
            .UseConsoleLifetime();
    }
}