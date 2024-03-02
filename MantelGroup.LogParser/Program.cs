using nietras.SeparatedValues;

namespace MantelGroup.LogParser
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Extensions.Print(SeqParserAlgorithm());
            Extensions.Print(CustomParserAlgorithm());
        }

        public static Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> CustomParserAlgorithm()
        {
            var IpAddressCounters = new Dictionary<string, int>();
            var UrlCounters = new Dictionary<string, int>();

            var lines = File.ReadLines("./Data/programming-task-example-data.txt");
            foreach (var line in lines)
            {
                var cols = line.Split(' ');

                var ipAddressColumn = cols[0];
                var urlDetailColumn = $"{cols[5]} {cols[6]} {cols[7]}".Trim('"');

                IpAddressCounters.AddOrIncrement(ipAddressColumn);
                UrlCounters.AddOrIncrement(urlDetailColumn);
            }

            return new(IpAddressCounters.Count, UrlCounters.GetTopXByValue(3), IpAddressCounters.GetTopXByValue(3));
        }

        public static Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> SeqParserAlgorithm()
        {
            var IpAddressCounters = new Dictionary<string, int>();
            var UrlCounters = new Dictionary<string, int>();

            "./Data/programming-task-example-data.txt".ReadThrough(reader =>
            {
                foreach (var row in reader)
                {
                    // Hover over reader, row or col when breaking here
                    var ipAddressColumn = row[0];
                    var urlDetailColumn = row[5];

                    IpAddressCounters.AddOrIncrement(ipAddressColumn.ToString());
                    UrlCounters.AddOrIncrement(urlDetailColumn.ToString());
                }
            });

            return new(IpAddressCounters.Count, UrlCounters.GetTopXByValue(3), IpAddressCounters.GetTopXByValue(3));
        }
    }
}

public static class Extensions
{
    public static void Print(Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> tuple)
    {
        Console.WriteLine($"\nNumber of unique IpAddress:{tuple.Item1}\n");

        Console.WriteLine("\nTop 3 Urls:\n");
        foreach (var urlCounter in tuple.Item2) Console.WriteLine($"{urlCounter.Key} - {urlCounter.Value}");

        Console.WriteLine("\nTop 3 IpAddress:\n");
        foreach (var orderedIpAddress in tuple.Item3) Console.WriteLine($"{orderedIpAddress.Key} - {orderedIpAddress.Value}");
    }
    public static void AddOrIncrement<T>(this IDictionary<T, int> someDictionary, T key)
        where T : notnull
    {
        someDictionary.TryGetValue(key, out var currentCount);
        someDictionary[key] = currentCount + 1;
    }

    public static List<KeyValuePair<T, int>> GetTopXByValue<T>(this IDictionary<T, int> someDictionary, int count)
        where T : notnull
    {
        return someDictionary.OrderByDescending(x => x.Value).Take(3).ToList();
    }

    public static void ReadThrough(this string filename, Action<SepReader> rowAction)
    {
        using var reader = Sep.New(' ')
            .Reader(o => o with
            {
                HasHeader = false,
                Unescape = true,
                DisableColCountCheck = true
            })
            .FromFile(filename);

        rowAction(reader);
    }
}