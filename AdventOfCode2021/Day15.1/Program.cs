﻿using System.Drawing;

string[] data = File.ReadAllLines("input.txt");

Dictionary<Point, int> cave = new();
for (int y = 0; y < data.Length; y++)
    for (int x = 0; x < data[y].Length; x++)
        cave.Add(new Point(x, y), (int)char.GetNumericValue(data[y][x]));

var topLeft = new Point(0, 0);
var bottomRight = new Point(cave.Keys.MaxBy(p => p.X).X, cave.Keys.MaxBy(p => p.Y).Y);

var queue = new PriorityQueue<Point, int>();
var calculationMap = new Dictionary<Point, int>();

calculationMap[topLeft] = 0;
queue.Enqueue(topLeft, 0);

while (queue.Count > 0)
{
    var currentPoint = queue.Dequeue();

    foreach (var movePoint in GetNextMove(currentPoint))
    {
        if (!calculationMap.ContainsKey(movePoint))
        {
            var totalRisk = calculationMap[currentPoint] + cave[movePoint];
            calculationMap[movePoint] = totalRisk;
            if (movePoint == bottomRight)
                break;

            queue.Enqueue(movePoint, totalRisk);
        }
    }
}

Console.WriteLine($"selected path: {calculationMap[bottomRight]}");

IEnumerable<Point> GetNextMove(Point p)
{
    // left
    if (p.X > 0)
        yield return new Point(p.X - 1, p.Y);

    // right
    if (p.X < bottomRight.X)
        yield return new Point(p.X + 1, p.Y);

    // up
    if (p.Y > 0)
        yield return new Point(p.X, p.Y - 1);

    // down
    if (p.Y < bottomRight.Y)
        yield return new Point(p.X, p.Y + 1);
}