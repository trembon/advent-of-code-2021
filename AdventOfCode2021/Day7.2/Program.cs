string data = File.ReadAllText("input.txt");

var crabPositions = data.Split(',').Select(x => int.Parse(x)).ToList();

List<int> fuelUsageCalculations = new List<int>();
for(int avg = crabPositions.Min(); avg <= crabPositions.Max(); avg++)
{
    int fuelUsage = 0;
    for(int i = 0; i < crabPositions.Count; i++)
        fuelUsage += Enumerable.Range(0, Math.Abs(crabPositions[i] - avg) + 1).Sum();

    fuelUsageCalculations.Add(fuelUsage);
}


Console.WriteLine($"fuel usage: {fuelUsageCalculations.Min()}");