using Orleans.TestingHost;
using Xunit;

namespace ZboxOrleans.Tests.Grains;

// Question: It takes extremely long time to process 1 test (14s), why?
[Collection("sequential")]
public abstract class GrainTestBase : IDisposable
{
    private TestCluster Cluster { get; }

    protected GrainTestBase()
    {
        var builder = new TestClusterBuilder();
        Cluster = builder.Build();
        Cluster.Deploy();
    }
    
    protected TGrain GetGrain<TGrain>(Guid primaryKey) where TGrain : IGrainWithGuidKey
    {
        return Cluster.GrainFactory.GetGrain<TGrain>(primaryKey);
    }

    public void Dispose() => Cluster.StopAllSilos();
}