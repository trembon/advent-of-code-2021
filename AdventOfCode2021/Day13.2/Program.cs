string[] data = File.ReadAllLines("input.txt");

string[] coordinates = data.TakeWhile(x => x.Contains(',')).ToArray();
string[] instructions = data.SkipWhile(x => !x.StartsWith("fold")).ToArray();

HashSet<(int x, int y)> grid = new();
foreach (var cord in coordinates)
{
    var split = cord.Split(',');
    grid.Add(new(int.Parse(split[0]), int.Parse(split[1])));
}

foreach (var intruction in instructions)
{
    var split = intruction.Split('=');
    var value = int.Parse(split[1]);
    if (split[0].EndsWith("y"))
    {
        foreach (var item in grid.Where(m => m.y > value).ToList())
        {
            grid.Add(new(item.x, value - (item.y - value)));
            grid.Remove(item);
        }
    }
    else if (split[0].EndsWith("x"))
    {
        foreach (var item in grid.Where(m => m.x > value).ToList())
        {
            grid.Add(new(value - (item.x - value), item.y));
            grid.Remove(item);
        }
    }
}

for (int y = 0; y <= grid.Max(k => k.y); y++)
{
    for (int x = 0; x <= grid.Max(k => k.x); x++)
    {
        Console.Write(grid.Contains(new(x, y)) ? "#" : ".");
    }
    Console.WriteLine();
}