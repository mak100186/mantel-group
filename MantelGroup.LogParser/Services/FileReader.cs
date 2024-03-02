namespace MantelGroup.LogParser.Services;

public interface IFileReader
{
    IEnumerable<string> ReadLines(string filename);
}

public class FileReader : IFileReader
{
    public IEnumerable<string> ReadLines(string filename)
    {
        return File.ReadLines(filename);
    }
}