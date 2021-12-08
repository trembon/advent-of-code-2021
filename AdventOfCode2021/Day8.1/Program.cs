string[] data = File.ReadAllLines("input.txt");

List<string> outputValues = new(data.Length);
foreach(var line in data)
{
    string[] split = line.Split(" | ");
    outputValues.AddRange(split[1].Split(" "));
}

int count = outputValues.Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);
Console.WriteLine($"counted output values: {count}");