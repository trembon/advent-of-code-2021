string[] data = File.ReadAllLines("input.txt");

List<int> outputValues = new(data.Length);
foreach (var line in data)
{
    var mappings = new string[10];

    string[] split = line.Split(" | ");
    var signal = split[0].Split(' ').ToList();

    mappings[1] = signal.Pick(x => x.Length == 2);
    mappings[7] = signal.Pick(x => x.Length == 3);
    mappings[4] = signal.Pick(x => x.Length == 4);
    mappings[8] = signal.Pick(x => x.Length == 7);

    mappings[3] = signal.Pick(x => x.Length == 5 && mappings[1].All(y => x.Contains(y)));

    mappings[9] = signal.Pick(x => x.Length == 6 && mappings[3].All(y => x.Contains(y)));
    mappings[0] = signal.Pick(x => x.Length == 6 && mappings[1].All(y => x.Contains(y)));
    mappings[6] = signal.Pick(x => x.Length == 6);

    mappings[5] = signal.Pick(x => x.All(y => mappings[9].Contains(y)));
    mappings[2] = signal.Pick(x => x.Length == 5);

    string output = "";
    foreach (var value in split[1].Split(' '))
        output += Array.IndexOf(mappings, new string(value.OrderBy(x => x).ToArray())).ToString();

    outputValues.Add(int.Parse(output));
}

Console.WriteLine($"sum of output: {outputValues.Sum()}");

static class ListExtensions
{
    public static string Pick(this List<string> list, Func<string, bool> where)
    {
        string item = list.Single(where);
        list.RemoveAt(list.IndexOf(item));
        return new string(item.OrderBy(x => x).ToArray());
    }
}