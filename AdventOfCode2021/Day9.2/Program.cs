string[] data = File.ReadAllLines("input.txt");


HashSet<(int x, int y)> basins = new();
for (int y = 0; y < data.Length; y++)
{
    for (int x = 0; x < data[y].Length; x++)
    {
        List<int> adjecents = new();
        if (x > 0)
        {
            int left = (int)char.GetNumericValue(data[y][x - 1]);
            adjecents.Add(left);
        }

        if (x < data[y].Length - 1)
        {
            int right = (int)char.GetNumericValue(data[y][x + 1]);
            adjecents.Add(right);
        }

        if (y > 0)
        {
            int up = (int)char.GetNumericValue(data[y - 1][x]);
            adjecents.Add(up);
        }

        if (y < data.Length - 1)
        {
            int down = (int)char.GetNumericValue(data[y + 1][x]);
            adjecents.Add(down);
        }

        int current = (int)char.GetNumericValue(data[y][x]);
        if (adjecents.Min() > current)
            basins.Add(new(x, y));
    }
}

List<int> basinSizes = new(basins.Count);
foreach(var basin in basins)
{
    HashSet<(int x, int y, int value)> result = new();

    CalculateBasin(data, basin.x, basin.y, result);

    basinSizes.Add(result.Count(x => x.value != 9));
}

var basinCalclation = basinSizes.OrderByDescending(x => x).Take(3).Aggregate((a, x) => a * x);
Console.WriteLine($"basin level calc: {0}");

void CalculateBasin(string[] data, int x, int y, HashSet<(int x, int y, int value)> result)
{
    int currentLevel = (int)char.GetNumericValue(data[y][x]);
    if (result.Contains(new(x, y, currentLevel)))
        return;

    result.Add(new(x, y, currentLevel));
    if (currentLevel == 9)
        return;

    if (x > 0)
        CalculateBasin(data, x - 1, y, result);

    if (x < data[y].Length - 1)
        CalculateBasin(data, x + 1, y, result);

    if (y > 0)
        CalculateBasin(data, x, y - 1, result);

    if (y < data.Length - 1)
        CalculateBasin(data, x, y + 1, result);
}