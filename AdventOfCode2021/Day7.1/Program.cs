string data = File.ReadAllText("input.txt");

var crabPositions = data.Split(',').Select(x => int.Parse(x)).ToList();

int median = crabPositions.OrderBy(x => x).ElementAt(crabPositions.Count / 2);
int fuelUsage = 0;
foreach(var position in crabPositions)
{
    int calc = median - position;
    fuelUsage += Math.Abs(calc);
}

Console.WriteLine($"fuel usage: {fuelUsage}");