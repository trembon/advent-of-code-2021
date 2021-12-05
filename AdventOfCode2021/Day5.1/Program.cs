string[] data = File.ReadAllLines("input.txt");

List<Line> lines = new(data.Length);
foreach(var lineData in data)
{
    string[] s1 = lineData.Split(" -> ");
    string[] p1 = s1[0].Split(',');
    string[] p2 = s1[1].Split(',');

    lines.Add(new Line(new Position(int.Parse(p1[0]), int.Parse(p1[1])), new Position(int.Parse(p2[0]), int.Parse(p2[1]))));
}

lines = lines.Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y).ToList();

Dictionary<Position, int> diagram = new();
foreach(var line in lines)
{
    int startX = line.Start.X > line.End.X ? line.End.X : line.Start.X;
    int endX = line.Start.X > line.End.X ? line.Start.X : line.End.X;
    if (startX != endX)
    {
        for (int x = startX; x <= endX; x++)
        {
            var position = new Position(x, line.Start.Y);
            if (!diagram.ContainsKey(position))
                diagram[position] = 0;

            diagram[position]++;
        }
    }

    int startY = line.Start.Y > line.End.Y ? line.End.Y : line.Start.Y;
    int endY = line.Start.Y > line.End.Y ? line.Start.Y : line.End.Y;
    if (startY != endY)
    {
        for (int y = startY; y <= endY; y++)
        {
            var position = new Position(line.Start.X, y);
            if (!diagram.ContainsKey(position))
                diagram[position] = 0;

            diagram[position]++;
        }
    }
}

int overlappingCount = diagram.Count(x => x.Value >= 2);
Console.WriteLine($"Overlapping count: {overlappingCount}");

record Position(int X, int Y);
record Line(Position Start, Position End);