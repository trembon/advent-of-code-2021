string data = File.ReadAllText("input.txt");

var fishes = data.Split(',').Select(x => new Lanternfish(int.Parse(x))).ToList();

for (int i = 0; i < 80; i++)
{
    List<Lanternfish> newFishes = new();
    foreach(var fish in fishes)
        if (fish.Decrease())
            newFishes.Add(new Lanternfish(8));

    fishes.AddRange(newFishes);
}

Console.WriteLine($"fish count: {fishes.Count}");

class Lanternfish
{
    public int State { get; private set; }

    public Lanternfish(int state)
    {
        State = state;
    }

    public bool Decrease()
    {
        if (State == 0)
        {
            State = 6;
            return true;
        }
        else
        {
            State--;
            return false;
        }
    }

    public override string ToString()
    {
        return $"{State}";
    }
}