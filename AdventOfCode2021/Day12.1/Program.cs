string[] data = File.ReadAllLines("input.txt");

Dictionary<string, CaveConnection> caves = new();
foreach(string line in data)
{
    var split = line.Split('-');

    if (!caves.ContainsKey(split[0]))
        caves.Add(split[0], new CaveConnection(split[0]));
    caves[split[0]].Connections.Add(split[1]);

    if (!caves.ContainsKey(split[1]))
        caves.Add(split[1], new CaveConnection(split[1]));
    caves[split[1]].Connections.Add(split[0]);
}

List<CavePath> paths = new();
VisitCave("start", new CavePath());

Console.WriteLine($"paths found: {paths.Count}");

void VisitCave(string id, CavePath path)
{
    var cave = caves[id];
    if (cave.IsSmall && path.Path.Contains(cave.Id))
        return;

    path.Path.Add(cave.Id);
    if (cave.IsEnd)
        return;

    for (int i = 0; i < cave.Connections.Count; i++)
    {
        var newPath = path.Clone();
        VisitCave(cave.Connections[i], newPath);

        if(newPath.Path.Last() == "end")
            paths.Add(newPath);
    }
}

class CavePath
{
    public List<string> Path { get; } = new();

    public CavePath Clone()
    {
        var path = new CavePath();
        path.Path.AddRange(Path);
        return path;
    }

    public override string ToString()
    {
        return string.Join(",", Path);
    }
}

class CaveConnection
{
    public string Id { get; }

    public List<string> Connections { get; } = new();

    public CaveConnection(string id)
    {
        Id = id;
    }

    public bool IsStart { get => Id == "start"; }
    public bool IsEnd { get => Id == "end"; }
    public bool IsSmall { get => char.IsLower(Id[0]); }

    public override string ToString()
    {
        return Id;
    }
}