using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZboxOrleans.Core.Constants;

namespace ZboxOrleans.Silo;

// 1. Orleans Silo: Nastavte prostředí pro Orleans Silo. Toto je základní krok, který je nezbytný pro další práci s Orleans.
public static class SiloHostBuilder
{
    private const string AzureBlobConnection = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://localhost:10000/devstoreaccount1;";
    private const string AzureTableConnection = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;TableEndpoint=http://localhost:10002/devstoreaccount1;";

    public static IHostBuilder Create(string[]? args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleans(silo =>
            {
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
            })
            .UseConsoleLifetime();
    }
}