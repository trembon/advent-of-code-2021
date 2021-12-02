string[] lines = File.ReadAllLines("input.txt");
int[] measurements = lines.Select(x => int.Parse(x)).ToArray();

const int slidingSize = 3;

List<int> slidingMeasurements = new();
for(int i = slidingSize - 1; i < measurements.Length; i++)
{
    Range range = new(i - (slidingSize - 1), i + 1);
    slidingMeasurements.Add(measurements[range].Sum());
}

int slidingMeasurementIncreases = 0;
for (int i = 1; i < slidingMeasurements.Count; i++)
    if (slidingMeasurements[i] > slidingMeasurements[i - 1])
        slidingMeasurementIncreases++;

Console.WriteLine($"measurementIncreases: {slidingMeasurementIncreases}");