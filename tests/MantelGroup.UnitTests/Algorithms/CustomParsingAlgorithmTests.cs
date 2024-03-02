namespace MantelGroup.UnitTests.Algorithms;

using Bogus;

using LogParser.Algorithms;
using LogParser.Services;

using Moq;

using Shouldly;

public class CustomParsingAlgorithmTests
{
    [Fact]
    public void Process_ReturnsExpectedResults_WhenCalled()
    {
        //Arrange
        var fakeFilename = new Faker().System.FileName();
        var mockFileReader = new Mock<IFileReader>();
        mockFileReader.Setup(x => x.ReadLines(It.Is<string>(f => f == fakeFilename)))
            .Returns(new List<string>
            {
                "192.168.0.1 - - - - GET /users HTTP/1.1",
                "192.168.0.1 - - - - GET /prices HTTP/1.1",
                "192.168.0.1 - - - - GET /prices HTTP/1.1",
                "192.168.0.1 - - - - POST /item HTTP/1.1",
                "192.168.0.1 - - - - GET /item HTTP/1.1",
                "192.168.0.2 - - - - GET /item/1 HTTP/1.1"
            });

        var algorithm = new CustomParsingAlgorithm(mockFileReader.Object);

        //Act
        var result = algorithm.Process(fakeFilename);

        //Assert
        result.Item1.ShouldBe(2);
        result.Item2.ShouldBe(new() { new("GET /prices HTTP/1.1", 2), new("GET /users HTTP/1.1", 1), new("POST /item HTTP/1.1", 1) });
        result.Item3.ShouldBe(new() { new("192.168.0.1", 5), new("192.168.0.2", 1) });

        mockFileReader.Verify(x => x.ReadLines(It.Is<string>(f => f == fakeFilename)), Times.Exactly(1));
    }
}