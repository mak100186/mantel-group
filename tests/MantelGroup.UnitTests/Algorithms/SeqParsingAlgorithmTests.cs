namespace MantelGroup.UnitTests.Algorithms;

using LogParser.Algorithms;

using Shouldly;

public class SeqParsingAlgorithmTests
{
    public static IEnumerable<object[]> Process_ReturnsExpectedResults_WhenCalled_Data()
    {
        yield return new object[]
        {
            "./Data/test-data-1.txt", 
            2, 
            new List<KeyValuePair<string, int>> { new("z", 3), new("y", 2), new("x", 1) }, 
            new List<KeyValuePair<string, int>> { new("b", 4), new("a", 2) }
        };

        yield return new object[]
        {
            "./Data/test-data-2.txt",
            2,
            new List<KeyValuePair<string, int>> { new("z", 3), new("y", 2), new("x", 1) },
            new List<KeyValuePair<string, int>> { new("a", 5), new("b", 1) }
        };
    }

    [Theory]
    [MemberData(nameof(Process_ReturnsExpectedResults_WhenCalled_Data))]
    public void Process_ReturnsExpectedResults_WhenCalled(string fileName, int countOfIpAddresses, List<KeyValuePair<string, int>> urlCounters, List<KeyValuePair<string, int>> ipAddressCounters)
    {
        //Arrange
        var algorithm = new SeqParsingAlgorithm();

        //Act
        var result = algorithm.Process(fileName);

        //Assert
        result.Item1.ShouldBe(countOfIpAddresses);
        result.Item2.ShouldBe(urlCounters);
        result.Item3.ShouldBe(ipAddressCounters);
    }
}