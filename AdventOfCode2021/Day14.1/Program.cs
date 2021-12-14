string[] data = File.ReadAllLines("input.txt");

string template = data[0];
Dictionary<string, string> rules = data.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(k => k.First(), v => v.Last());

for(int i = 0; i < 10; i++)
{
    for(int j = 0; j < template.Length - 1; j++)
    {
        string pair = template.Substring(j, 2);
        if (rules.ContainsKey(pair))
        {
            template = template.Insert(j + 1, rules[pair]);
            j++;
        }
    }
}

var result = template.GroupBy(x => x).Select(x => new { Element = x.Key, Count = x.Count() }).OrderBy(x => x.Count).ToList();
Console.WriteLine($"element calc: {result.Last().Count - result.First().Count}");