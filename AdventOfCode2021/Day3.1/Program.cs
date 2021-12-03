string[] diagnostics = File.ReadAllLines("input.txt");


List<char> gamma = new();
List<char> epsilon = new();
for(int i = 0; i < diagnostics[0].Length; i++)
{
    var common = diagnostics.Select(x => x[i]).GroupBy(x => x).Select(x => new { x.Key, Count = x.Count() }).OrderByDescending(x => x.Count).ToList();
    gamma.Add(common[0].Key);
    epsilon.Add(common[1].Key);
}

int gammaValue = Convert.ToInt32(new string(gamma.ToArray()), 2);
int epsilonValue = Convert.ToInt32(new string(epsilon.ToArray()), 2);

Console.WriteLine($"consumption: {gammaValue * epsilonValue}");