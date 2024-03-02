namespace MantelGroup.LogParser.Algorithms.Abstractions;

public interface IParsingAlgorithm
{
    Tuple<int, List<KeyValuePair<string, int>>, List<KeyValuePair<string, int>>> Process(string filename);
}