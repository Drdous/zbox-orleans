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

    protected async Task<TGrain> GetGrainAsync<TGrain>(Guid primaryKey) where TGrain : IGrainWithGuidKey
    {
        if (_siloHost is null || _clientHost is null)
        {
            _siloHost = SiloHostBuilder.Create().Build();
            _clientHost = ClientHostBuilder.Create().Build();
            
            await _siloHost.StartAsync();
            await _clientHost.StartAsync();
        }
        
        var client = _clientHost.Services.GetRequiredService<IGrainFactory>();

        return client.GetGrain<TGrain>(primaryKey);
    }

    public void Dispose()
    {
        _clientHost?.Dispose();
        _siloHost?.Dispose();
    }
}