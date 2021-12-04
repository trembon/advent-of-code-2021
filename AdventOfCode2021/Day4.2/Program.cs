string[] data = File.ReadAllLines("input.txt");
int[] sequence = data[0].Split(',').Select(x => int.Parse(x)).ToArray();

List<Playboard> boards = new();
for (int i = 2; data.Length > i; i += 6)
{
    int[,] boardData = new int[5, 5];
    for (int j = 0; 5 > j; j++)
    {
        boardData[j, 0] = int.Parse(data[i + j].Substring(0, 2).Trim());
        boardData[j, 1] = int.Parse(data[i + j].Substring(3, 2).Trim());
        boardData[j, 2] = int.Parse(data[i + j].Substring(6, 2).Trim());
        boardData[j, 3] = int.Parse(data[i + j].Substring(9, 2).Trim());
        boardData[j, 4] = int.Parse(data[i + j].Substring(12, 2).Trim());
    }
    boards.Add(new Playboard(boardData));
}

int winnerBall = 0;
foreach (int ball in sequence)
{
    boards.ForEach(x => x.Stamp(ball));

    var winners = boards.Where(x => x.IsComplete()).ToArray();
    if(winners.Any() && boards.Count == 1)
    {
        winnerBall = ball;
        break;
    }
    else
    {
        foreach(var winner in winners)
            boards.Remove(winner);
    }
}

int unstampedValues = boards.Single().Items.Where(pi => !pi.IsStamped).Sum(pi => pi.Value);
Console.WriteLine($"final score: {unstampedValues * winnerBall}");

class Playboard
{
    Dictionary<int, PlayboardItem> items = new();
    Dictionary<string, List<PlayboardItem>> indexes = new();

    public IEnumerable<PlayboardItem> Items => items.Values;

    public Playboard(int[,] board)
    {
        for (int x = 0; board.GetLength(0) > x; x++)
        {
            indexes[$"X{x}"] = new List<PlayboardItem>();

            for (int y = 0; board.GetLength(1) > y; y++)
            {
                if (x == 0)
                    indexes[$"Y{y}"] = new List<PlayboardItem>();

                var item = new PlayboardItem
                {
                    X = x,
                    Y = y,
                    Value = board[x, y]
                };
                items.Add(item.Value, item);

                indexes[$"X{x}"].Add(item);
                indexes[$"Y{y}"].Add(item);
            }
        }
    }

    public void Stamp(int value)
    {
        if (items.ContainsKey(value))
            items[value].IsStamped = true;
    }

    public bool IsComplete()
    {
        foreach (var index in indexes)
            if (index.Value.All(pi => pi.IsStamped))
                return true;

        return false;
    }
}

class PlayboardItem
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }
    public bool IsStamped { get; set; }
}