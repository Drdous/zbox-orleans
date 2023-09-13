using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZboxOrleans.Silo;
using Xunit;

namespace ZboxOrleans.Tests.Grains;

[Collection("Sequential")]
public abstract class BaseGrainTest : IDisposable
{
    private IHost? _siloHost;
    protected IGrainFactory? GrainFactory;

    protected async Task<TGrain> GetGrainAsync<TGrain>(Guid primaryKey) where TGrain : IGrainWithGuidKey
    {
        await InitializeIfNotExistAsync();
        return GrainFactory!.GetGrain<TGrain>(primaryKey);
    }

    protected async Task InitializeIfNotExistAsync()
    {
        if (_siloHost is null)
        {
            await InitializeHostsAsync();
            GrainFactory ??= GetService<IGrainFactory>();
        }
    }
    
    protected T GetService<T>() where T : notnull
    {
        return _siloHost!.Services.GetRequiredService<T>();
    }
 
    // I know it's not the right way to test (dependency on running services, azurite..), but it's better for me in terms of debugging purposes and looking to the blob storage.
    private async Task InitializeHostsAsync()
    {
        _siloHost = SiloHostBuilder.Create().Build();         
        await _siloHost.StartAsync();
    }

    public void Dispose()
    {
        _siloHost?.Dispose();
    }
}