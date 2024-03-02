namespace MantelGroup.LogParser;

using Algorithms.Abstractions;

using Extensions;

public static class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "./Data/programming-task-example-data.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"No data file found. Expected location: [{filePath}]. Press any key to exit.");
            _ = Console.ReadKey();
            Environment.Exit(0);
        }

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IParsingAlgorithm).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
            .ToList();

        while (true)
        {
            Console.WriteLine("Available parsing algorithms are: ");
            for (var index = 0; index < types.Count; index++)
            {
                var type = types[index];
                Console.WriteLine($"{index + 1} - {type.Name}");
            }

            Console.Write("Type 0 to Exit or Number or Name of the algorithm to run: ");
            var typeInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(typeInput))
            {
                Console.WriteLine($"Invalid input:[{typeInput}]");

                continue;
            }

            Type? selectedType = null;
            if (int.TryParse(typeInput, out var inputInteger))
            {
                switch (inputInteger)
                {
                    case 0:
                        Environment.Exit(0);

                        break;
                    default:
                        if (inputInteger < 0 || inputInteger > types.Count)
                        {
                            Console.WriteLine($"Type not found: [{typeInput}]");

                            continue;
                        }

                        selectedType = types[inputInteger - 1];

                        break;
                }
            }
            else
            {
                selectedType = types.FirstOrDefault(x => x.Name == typeInput);
            }

            if (selectedType == null)
            {
                Console.WriteLine($"Type not found: [{typeInput}]");

                continue;
            }

            var algorithmInstance = (IParsingAlgorithm)Activator.CreateInstance(selectedType)!;

            algorithmInstance.Print(filePath);
            
            Console.WriteLine("Press any key to clear the screen");
            _ = Console.ReadKey();
            Console.Clear();
        }
    }
}