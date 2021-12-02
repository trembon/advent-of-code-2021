string[] instructions = File.ReadAllLines("input.txt");

int depthPosition = 0;
int horizontalPosition = 0;
int aim = 0;

foreach (var instruction in instructions)
{
    string[] vs = instruction.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    int value = int.Parse(vs[1]);

    switch (vs[0])
    {
        case "up":
            aim -= value;
            break;

        case "down":
            aim += value;
            break;

        case "forward":
            horizontalPosition += value;
            depthPosition += aim * value;
            break;
    }
}

Console.WriteLine("---Result---");
Console.WriteLine($"Depth: {depthPosition}");
Console.WriteLine($"Horizontal: {horizontalPosition}");
Console.WriteLine($"Calculation: {depthPosition * horizontalPosition}");