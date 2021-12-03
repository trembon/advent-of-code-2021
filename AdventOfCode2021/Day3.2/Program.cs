string[] diagnostics = File.ReadAllLines("input.txt");

List<string> oxygenRatings = diagnostics.ToList();
List<string> scrubberRatings = diagnostics.ToList();

for (int i = 0; i < diagnostics[0].Length && oxygenRatings.Count > 1; i++)
    oxygenRatings = oxygenRatings.GroupBy(x => x[i]).OrderByDescending(x => x.Count()).First().ToList();

for (int i = 0; i < diagnostics[0].Length && scrubberRatings.Count > 1; i++)
    scrubberRatings = scrubberRatings.GroupBy(x => x[i]).OrderBy(x => x.Count()).First().ToList();

int oxygenValue = Convert.ToInt32(oxygenRatings[0], 2);
int scrubberValue = Convert.ToInt32(scrubberRatings[0], 2);

Console.WriteLine($"lifeSupport: {oxygenValue * scrubberValue}");