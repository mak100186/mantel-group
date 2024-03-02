using MantelGroup.LogParser.Extensions;

namespace MantelGroup.LogParser.Algorithms;

using System.Collections.Concurrent;
using Abstractions;

public class ParallelParsingAlgorithm : IParsingAlgorithm
{
    public Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> Process(string filename)
    {
        var ipAddressCounters = new ConcurrentDictionary<string, int>();
        var urlCounters = new ConcurrentDictionary<string, int>();

        var allLines = new string[24]; //only allocate memory here. wont work if the line count is different

        using (StreamReader sr = File.OpenText(filename))
        {
            var x = 0;
            while (!sr.EndOfStream)
            {
                allLines[x] = sr.ReadLine();
                x += 1;
            }
        } 

        Parallel.For(0,
            allLines.Length,
            x =>
            {
                var line = allLines[x];

                var cols = line.Split(' ');

                var ipAddressColumn = cols[0];
                var urlDetailColumn = $"{cols[5]} {cols[6]} {cols[7]}".Trim('"');

                ipAddressCounters.AddOrUpdate(ipAddressColumn, 1, (_, oldValue) => oldValue + 1);
                urlCounters.AddOrUpdate(urlDetailColumn, 1, (_, oldValue) => oldValue + 1);
            });

            

        return new(ipAddressCounters.Count, urlCounters.GetTopXByValue(3), ipAddressCounters.GetTopXByValue(3));
    }
}