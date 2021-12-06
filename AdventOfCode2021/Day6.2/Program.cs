string data = File.ReadAllText("input.txt");

List<long> fishCycles = Enumerable.Range(0,9).Select(x => (long)0).ToList();

foreach(var initial in data.Split(',').Select(x => int.Parse(x)))
    fishCycles[initial]++;

for(int i = 0; i < 256; i++)
{
    long newFishes = fishCycles[0];
    fishCycles = fishCycles.Skip(1).ToList();

    fishCycles[6] += newFishes;
    fishCycles.Add(newFishes);
}

Console.WriteLine($"fish count: {fishCycles.Sum()}");