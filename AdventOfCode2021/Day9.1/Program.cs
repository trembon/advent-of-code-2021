string[] data = File.ReadAllLines("input.txt");

List<int> riskLevels = new();
for(int y = 0; y < data.Length; y++)
{
    for(int x = 0; x < data[y].Length; x++)
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
            riskLevels.Add(current + 1);
    }
}

Console.WriteLine($"risk level sum: {riskLevels.Sum()}");