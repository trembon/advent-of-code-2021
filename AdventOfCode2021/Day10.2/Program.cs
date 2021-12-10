string[] lines = File.ReadAllLines("input.txt");

List<char> openingChunks = new() { '(', '{', '[', '<' };
List<char> closingChunks = new() { ')', '}', ']', '>' };

List<long> autoCompleteValues = new();
foreach (var line in lines)
{
    Stack<char> openChunks = new();
    for (int i = 0; i < line.Length; i++)
    {
        if (openingChunks.Contains(line[i]))
        {
            openChunks.Push(line[i]);
        }
        else
        {
            int chunkIndex = openingChunks.IndexOf(openChunks.Pop());
            if (closingChunks[chunkIndex] != line[i])
            {
                openChunks.Clear();
                break;
            }
        }
    }

    long autoCompleteValue = 0;
    while (openChunks.Count > 0)
    {
        autoCompleteValue *= 5;
        int chunkIndex = openingChunks.IndexOf(openChunks.Pop());
        switch (closingChunks[chunkIndex])
        {
            case ')': autoCompleteValue += 1; break;
            case ']': autoCompleteValue += 2; break;
            case '}': autoCompleteValue += 3; break;
            case '>': autoCompleteValue += 4; break;
        }
    }

    if(autoCompleteValue > 0)
        autoCompleteValues.Add(autoCompleteValue);
}

long winner = autoCompleteValues.OrderBy(x => x).ElementAt(autoCompleteValues.Count / 2);

Console.WriteLine($"syntax error values: {winner}");