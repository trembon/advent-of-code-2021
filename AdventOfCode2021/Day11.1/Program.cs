using System.Collections.Generic;

string[] data = File.ReadAllLines("input.txt");
int[,] grid = new int[data.Length, data[0].Length];

for (int y = 0; y < data.Length; y++)
    for (int x = 0; x < data[y].Length; x++)
        grid[y, x] = (int)char.GetNumericValue(data[y][x]);

int totalFlashes = 0;
for (int i = 0; i < 100; i++)
{
    HashSet<(int y, int x)> flashed = new();
    for (int y = 0; y < grid.GetLength(0); y++)
        for (int x = 0; x < grid.GetLength(1); x++)
            IncreaseAndFlash(y, x, flashed);

    foreach (var flash in flashed)
        grid[flash.y, flash.x] = 0;

    totalFlashes += flashed.Count;
}

Console.WriteLine("total flashes " + totalFlashes);

void IncreaseAndFlash(int y, int x, HashSet<(int y, int x)> flashed)
{
    grid[y, x]++;
    if (grid[y, x] > 9 && !flashed.Contains(new(y, x)))
    {
        flashed.Add(new(y, x));
        ProcessAdjecents(y, x, flashed);
    }
}

void ProcessAdjecents(int y, int x, HashSet<(int y, int x)> flashed)
{
    // left
    if (x > 0)
        IncreaseAndFlash(y, x - 1, flashed);

    // right
    if (x < data[y].Length - 1)
        IncreaseAndFlash(y, x + 1, flashed);

    // up
    if (y > 0)
        IncreaseAndFlash(y - 1, x, flashed);

    // down
    if (y < data.Length - 1)
        IncreaseAndFlash(y + 1, x, flashed);

    // left - up
    if(x > 0 && y > 0)
        IncreaseAndFlash(y - 1, x - 1, flashed);

    // left - down
    if (x > 0 && y < data.Length - 1)
        IncreaseAndFlash(y + 1, x - 1, flashed);

    // right - up
    if (x < data[y].Length - 1 && y > 0)
        IncreaseAndFlash(y - 1, x + 1, flashed);

    // right - down
    if (x < data[y].Length - 1 && y < data.Length - 1)
        IncreaseAndFlash(y + 1, x + 1, flashed);
}