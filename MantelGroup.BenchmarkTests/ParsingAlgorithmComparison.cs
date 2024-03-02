using BenchmarkDotNet.Attributes;

using MantelGroup.LogParser.Algorithms;

namespace MantelGroup.BenchmarkTests;

using LogParser.Services;

[RankColumn]
[MemoryDiagnoser]
public class ParsingAlgorithmComparison
{
    private const string Filename = "./Data/programming-task-example-data.txt";

    [Benchmark]
    public void CustomParsingAlgorithm()
    {
        _ = new CustomParsingAlgorithm(new FileReader()).Process(Filename);
    }

    [Benchmark]
    public void SeqParsingAlgorithm()
    {
        _ = new SeqParsingAlgorithm().Process(Filename);
    }

    [Benchmark]
    public void StreamReadParsingAlgorithm()
    {
        _ = new StreamReadParsingAlgorithm().Process(Filename);
    }

    [Benchmark]
    public void StreamTextParsingAlgorithm()
    {
        _ = new StreamTextParsingAlgorithm().Process(Filename);
    }

    [Benchmark]
    public void ParallelParsingAlgorithm()
    {
        _ = new ParallelParsingAlgorithm().Process(Filename);
    }
}