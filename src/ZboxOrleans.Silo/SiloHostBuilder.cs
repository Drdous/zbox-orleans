using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using ZboxOrleans.Core.Constants;
using ZboxOrleans.Silo.Helpers;

namespace ZboxOrleans.Silo;

// 1. Orleans Silo: Nastavte prostředí pro Orleans Silo. Toto je základní krok, který je nezbytný pro další práci s Orleans.

// Note for me: We can choose which grains can be activated on which silo:
// https://learn.microsoft.com/en-us/dotnet/orleans/host/heterogeneous-silos
public static class SiloHostBuilder
{
    private const string AzureBlobConnection = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://localhost:10000/devstoreaccount1;";
    private const string AzureTableConnection = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;TableEndpoint=http://localhost:10002/devstoreaccount1;";

    public static IHostBuilder Create(string[]? args = null)
    {
        var siloEndpoint = SiloConfigHelper.GetSiloEndpointConfig(args);
        
        return Host.CreateDefaultBuilder(args)
            .UseOrleans(silo =>
            {
                silo.Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "Cluster42";
                    options.ServiceId = "MyAwesomeService";
                });
                
                silo.UseLocalhostClustering()
                    .ConfigureLogging(logging => logging.AddConsole());
                
                silo.AddAzureBlobGrainStorage(
                    name: Constants.ProviderNames.AzureBlobStorage,
                    configureOptions: options =>
                    {
                        options.ConfigureBlobServiceClient(AzureBlobConnection);
                    });

                silo.UseTransactions();
                silo.UseAzureTableReminderService(AzureTableConnection);
                
                // 10. Orleans Dashboard: Integrujte svůj projekt s Orleans Dashboard pro lepší monitorování a ladění.
                silo.UseDashboard(options => options.HostSelf = true);

                silo.ConfigureEndpoints(siloEndpoint.SiloPort, siloEndpoint.GatewayPort);
                
            })
            .UseConsoleLifetime();
    }
}