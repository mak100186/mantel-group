namespace MantelGroup.LogParser.Extensions;

using Algorithms.Abstractions;

using ConsoleTables;

using nietras.SeparatedValues;

public static class Extensions
{
    public static void Print(this IParsingAlgorithm algorithm, string filename)
    {
        Console.WriteLine("==================================== Output =====================================================");
        var tuple = algorithm.Process(filename);

        Console.WriteLine($"\nThe number of unique IP addresses: {tuple.Item1}");

        Console.WriteLine("\nThe top 3 most visited URLs");
        ConsoleTable
            .From(tuple.Item2)
            .Configure(o =>
            {
                o.NumberAlignment = Alignment.Right;
            })
            .Write(Format.Minimal);

        Console.WriteLine("\nThe top 3 most active IP addresses");
        ConsoleTable
            .From(tuple.Item3)
            .Configure(o =>
            {
                o.NumberAlignment = Alignment.Right;
            })
            .Write(Format.Minimal);
        
        Console.WriteLine("====================================END OF RUN=====================================================");
    }

    public static void AddOrIncrement(this IDictionary<string, int> dictionary, string key)
    {
        dictionary.TryGetValue(key, out var currentCount);
        dictionary[key] = currentCount + 1;
    }

    public static List<KeyValuePair<string, int>> GetTopXByValue(this IDictionary<string, int> dictionary, int count)
    {
        return dictionary.OrderByDescending(x => x.Value).Take(count).ToList();
    }
}