string[] data = File.ReadAllLines("input.txt");

List<Line> lines = new(data.Length);
foreach (var lineData in data)
{
    string[] s1 = lineData.Split(" -> ");
    string[] p1 = s1[0].Split(',');
    string[] p2 = s1[1].Split(',');

    lines.Add(new Line(new Position(int.Parse(p1[0]), int.Parse(p1[1])), new Position(int.Parse(p2[0]), int.Parse(p2[1]))));
}

var evenLines = lines.Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y).ToList();
var oddLines = lines.Where(l => l.Start.X != l.End.X && l.Start.Y != l.End.Y).ToList();

Dictionary<Position, int> diagram = new();
foreach (var line in evenLines)
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

Func<int, int, int> add = (x, y) => x + y;
Func<int, int, int> minus = (x, y) => x - y;

foreach (var line in oddLines)
{
    Func<int, int, int> xCalc = line.Start.X > line.End.X ? minus : add;
    Func<int, int, int> yCalc = line.Start.Y > line.End.Y ? minus : add;

    int diff = Math.Abs(line.Start.X - line.End.X);

    for(int i = 0; i <= diff; i++)
    {
        var position = new Position(xCalc(line.Start.X, i), yCalc(line.Start.Y, i));
        if (!diagram.ContainsKey(position))
            diagram[position] = 0;

        diagram[position]++;
    }
}

int overlappingCount = diagram.Count(x => x.Value >= 2);
Console.WriteLine($"Overlapping count: {overlappingCount}");

record Position(int X, int Y);
record Line(Position Start, Position End);