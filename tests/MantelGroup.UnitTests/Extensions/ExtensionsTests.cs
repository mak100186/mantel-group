namespace MantelGroup.UnitTests.Extensions;

using LogParser.Extensions;

using Shouldly;

public class ExtensionsTests
{
    public static IEnumerable<object[]> GetTopXByValue_Data()
    {
        //use case: if the counts are the same, then the input order is preserved
        yield return new object[]
        {
            new Dictionary<string, int>{ {"a", 5}, {"b", 5}, {"c", 5} , {"d", 5}}, 3, new List<string> {"a", "b", "c"}
        };

        //use case: the result is in descending order
        yield return new object[]
        {
            new Dictionary<string, int>{ {"a", 4}, {"b", 5}, {"c", 2} , {"d", 1}}, 3, new List<string> {"b", "a", "c"}
        };

        //use case: if the input values are less than the sought count then return the existing ones in a descending order
        yield return new object[]
        {
            new Dictionary<string, int>{ {"a", 4}, {"b", 5}}, 3, new List<string> {"b", "a"}
        };
    }

    [Theory]
    [MemberData(nameof(GetTopXByValue_Data))]
    public void GetTopXByValue_ReturnsTopRecords_WhenCalled(Dictionary<string, int> dictionary, int count, List<string> expectedResults)
    {
        //Arrange

        //Act
        var result = dictionary.GetTopXByValue(count);

        //Assert
        result.Count.ShouldBe(expectedResults.Count);
        result.Select(x => x.Key).ShouldBe(expectedResults);
    }

    public static IEnumerable<object[]> AddOrIncrement_Data()
    {
        //use case: adds the keys when not found and sets the counter to 1
        yield return new object[]
        {
            new List<string>{ "a", "b", "c" }, new Dictionary<string, int> { { "a", 1 }, { "b", 1 }, { "c", 1 } }
        };

        //use case: increments the key counter when same keys is added multiple times
        yield return new object[]
        {
            new List<string>{ "a", "b", "c", "c" }, new Dictionary<string, int> { { "a", 1 }, { "b", 1 }, { "c", 2 } }
        };

        //use case: increments the key counter when multiple keys are added multiple times
        yield return new object[]
        {
            new List<string>{ "a", "a", "b", "c", "c" }, new Dictionary<string, int> { { "a", 2 }, { "b", 1 }, { "c", 2 } }
        };
    }

    [Theory]
    [MemberData(nameof(AddOrIncrement_Data))]
    public void AddOrIncrement_AddsOrIncrementsKeys_WhenCalled(List<string> entries, Dictionary<string, int> expectedResult)
    {
        //Arrange
        var subject = new Dictionary<string, int>();

        //Act
        foreach (var entry in entries)
        {
            subject.AddOrIncrement(entry);
        }
        
        //Assert
        subject.Count.ShouldBe(expectedResult.Count);
        subject.ShouldBe(expectedResult);
    }
}