using System.Text;
using MantelGroup.LogParser.Algorithms.Abstractions;
using MantelGroup.LogParser.Extensions;

namespace MantelGroup.LogParser.Algorithms;

public class StreamReadParsingAlgorithm : IParsingAlgorithm
{
    public Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> Process(string filename)
    {
        var ipAddressCounters = new Dictionary<string, int>();
        var urlCounters = new Dictionary<string, int>();

        const int bufferSize = 128;
        using (var fileStream = File.OpenRead(filename))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
        {
            while (streamReader.ReadLine() is { } line)
            {
                var cols = line.Split(' ');

                var ipAddressColumn = cols[0];
                var urlDetailColumn = $"{cols[5]} {cols[6]} {cols[7]}".Trim('"');

                ipAddressCounters.AddOrIncrement(ipAddressColumn);
                urlCounters.AddOrIncrement(urlDetailColumn);
            }
        }
        
        return new(ipAddressCounters.Count, urlCounters.GetTopXByValue(3), ipAddressCounters.GetTopXByValue(3));
    }
}