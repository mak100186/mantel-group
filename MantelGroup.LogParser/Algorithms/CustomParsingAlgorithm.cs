﻿using MantelGroup.LogParser.Algorithms.Abstractions;
using MantelGroup.LogParser.Extensions;
using MantelGroup.LogParser.Services;

namespace MantelGroup.LogParser.Algorithms;

public class CustomParsingAlgorithm(IFileReader fileReader) : IParsingAlgorithm
{
    public Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> Process(string filename)
    {
        var ipAddressCounters = new Dictionary<string, int>();
        var urlCounters = new Dictionary<string, int>();

        var lines = fileReader.ReadLines(filename);
        foreach (var line in lines)
        {
            var cols = line.Split(' ');

            var ipAddressColumn = cols[0];
            var urlDetailColumn = $"{cols[5]} {cols[6]} {cols[7]}".Trim('"');

            ipAddressCounters.AddOrIncrement(ipAddressColumn);
            urlCounters.AddOrIncrement(urlDetailColumn);
        }

        return new(ipAddressCounters.Count, urlCounters.GetTopXByValue(3), ipAddressCounters.GetTopXByValue(3));
    }
}

