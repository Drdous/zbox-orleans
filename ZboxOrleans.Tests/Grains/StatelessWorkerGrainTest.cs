using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;
using ZboxOrleans.GrainInterfaces;

namespace ZboxOrleans.Tests.Grains;

public sealed class StatelessWorkerGrainTest : BaseGrainTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StatelessWorkerGrainTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // Throughput around 700k requests per second
    [Fact]
    public async Task TestStatelessWorkerGrainMaximumThroughput_WriteInfoMessageAsOutput()
    {
        await InitializeIfNotExist();

        const int numberOfCalls = 1_000_000;

        var sw = new Stopwatch();
        sw.Start();
        
        Parallel.For(0, numberOfCalls, new ParallelOptions { MaxDegreeOfParallelism = 1000 }, _ =>
        {
            GrainFactory!.GetGrain<IStatelessWorkerGrain>(Guid.NewGuid());
        });
        
        sw.Stop();
        
        _testOutputHelper.WriteLine($"Throughput result: {Math.Round(numberOfCalls / sw.Elapsed.TotalSeconds)} rps.");
    }
}