using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZboxOrleans.Client;
using ZboxOrleans.Silo;
using Xunit;

namespace ZboxOrleans.Tests.Grains;

[Collection("Sequential")]
public abstract class BaseGrainTest : IDisposable
{
    private IHost? _siloHost;
    private IHost? _clientHost;
    protected IGrainFactory? GrainFactory;

    protected async Task<TGrain> GetGrainAsync<TGrain>(Guid primaryKey) where TGrain : IGrainWithGuidKey
    {
        await InitializeIfNotExist();
        return GrainFactory!.GetGrain<TGrain>(primaryKey);
    }

    protected async Task InitializeIfNotExist()
    {
        if (_siloHost is null || _clientHost is null)
        {
            await InitializeHosts();
        }
        
        GrainFactory ??= _clientHost!.Services.GetRequiredService<IGrainFactory>();
    }

    private async Task InitializeHosts()
    {
        _siloHost = SiloHostBuilder.Create().Build();
        _clientHost = ClientHostBuilder.Create().Build();
            
        await _siloHost.StartAsync();
        await _clientHost.StartAsync();
    }

    public void Dispose()
    {
        _clientHost?.Dispose();
        _siloHost?.Dispose();
    }
}