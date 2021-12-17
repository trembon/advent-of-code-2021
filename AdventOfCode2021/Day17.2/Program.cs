using System.Text.RegularExpressions;

string data = File.ReadAllText("input.txt");

var matchX = Regex.Match(data, "x=(-?\\d*)\\.\\.(\\d*),");
var matchY = Regex.Match(data, "y=(-?\\d*)\\.\\.(-?\\d*)");

int[] rangeX = new int[] { int.Parse(matchX.Groups[1].Value), int.Parse(matchX.Groups[2].Value) }.OrderBy(x => x).ToArray();
int[] rangeY = new int[] { int.Parse(matchY.Groups[1].Value), int.Parse(matchY.Groups[2].Value) }.OrderBy(x => x).ToArray();

List<string> foundVelocities = new();

int xLower = 1;
while ((xLower * (xLower + 1)) / 2 < rangeX.First()) xLower++;

for (int x = xLower; x <= rangeX.Last(); x++)
{
    for (int y = rangeY.First(); y < Math.Abs(rangeY.First()); y++)
    {
        int xVel = x, yVel = y;
        int xPos = 0, yPos = 0;

        while (yPos >= rangeY.First() && xPos <= rangeX.Last())
        {
            xPos += xVel; yPos += yVel;
            if (xPos >= rangeX.First() && xPos <= rangeX.Last() && yPos <= rangeY.Last() && yPos >= rangeY.First())
            {
                foundVelocities.Add(x + "," + y);
            }
            xVel = (xVel == 0) ? 0 : xVel - 1;
            yVel--;
        }
    }
}

Console.WriteLine($"combinations: {foundVelocities.Distinct().Count()}");