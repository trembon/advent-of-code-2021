string[] lines = File.ReadAllLines("input.txt");
int[] measurements = lines.Select(x => int.Parse(x)).ToArray();

int measurementIncreases = 0;
for(int i = 1; i < measurements.Length; i++)
    if(measurements[i] > measurements[i - 1])
        measurementIncreases++;

Console.WriteLine($"measurementIncreases: {measurementIncreases}");