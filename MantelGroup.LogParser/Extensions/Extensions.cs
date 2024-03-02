namespace MantelGroup.LogParser.Extensions;

using Algorithms.Abstractions;
using nietras.SeparatedValues;

public static class Extensions
{
    public static void Print(this IParsingAlgorithm algorithm, string filename)
    {
        var tuple = algorithm.Process(filename);
        Console.WriteLine($"\nNumber of unique IpAddress:{tuple.Item1}\n");

        Console.WriteLine("\nTop 3 Urls:\n");
        foreach (var urlCounter in tuple.Item2) Console.WriteLine($"{urlCounter.Key} - {urlCounter.Value}");

        Console.WriteLine("\nTop 3 IpAddress:\n");
        foreach (var orderedIpAddress in tuple.Item3) Console.WriteLine($"{orderedIpAddress.Key} - {orderedIpAddress.Value}");
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