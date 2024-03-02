using MantelGroup.LogParser.Algorithms.Abstractions;
using MantelGroup.LogParser.Extensions;

using nietras.SeparatedValues;

namespace MantelGroup.LogParser.Algorithms;

public class SeqParsingAlgorithm : IParsingAlgorithm
{
    public Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> Process(string filename)
    {
        var ipAddressCounters = new Dictionary<string, int>();
        var urlCounters = new Dictionary<string, int>();

        using var reader = Sep.New(' ')
            .Reader(o => o with
            {
                HasHeader = false,
                Unescape = true,
                DisableColCountCheck = true
            })
            .FromFile(filename);
        
        foreach (var row in reader)
        {
            // Hover over reader, row or col when breaking here
            var ipAddressColumn = row[0];
            var urlDetailColumn = row[5];

            ipAddressCounters.AddOrIncrement(ipAddressColumn.ToString());
            urlCounters.AddOrIncrement(urlDetailColumn.ToString());
        }
        
        return new(ipAddressCounters.Count, urlCounters.GetTopXByValue(3), ipAddressCounters.GetTopXByValue(3));
    }
}