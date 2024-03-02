using MantelGroup.LogParser.Algorithms;
using MantelGroup.LogParser.Extensions;

namespace MantelGroup.LogParser;

using Services;

public static class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "./Data/programming-task-example-data.txt";

        Console.WriteLine($"Running {nameof(SeqParsingAlgorithm)}");
        new SeqParsingAlgorithm().Print(filePath);

        Console.WriteLine($"Running {nameof(CustomParsingAlgorithm)}");
        new CustomParsingAlgorithm(new FileReader()).Print(filePath);
    }
}