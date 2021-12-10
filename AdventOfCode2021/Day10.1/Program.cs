string[] lines = File.ReadAllLines("input.txt");

List<char> openingChunks = new() { '(', '{', '[', '<' };
List<char> closingChunks = new() { ')', '}', ']', '>' };

List<char> invalidChunkEndings = new();
foreach (var line in lines)
{
    Stack<char> openChunks = new();
    for(int i = 0; i < line.Length; i++)
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
                invalidChunkEndings.Add(line[i]);
                break;
            }
        }
    }
}

int ending1 = invalidChunkEndings.Count(x => x == ')') * 3;
int ending2 = invalidChunkEndings.Count(x => x == ']') * 57;
int ending3 = invalidChunkEndings.Count(x => x == '}') * 1197;
int ending4 = invalidChunkEndings.Count(x => x == '>') * 25137;

Console.WriteLine($"syntax error values: {ending1 + ending2 + ending3 + ending4}");