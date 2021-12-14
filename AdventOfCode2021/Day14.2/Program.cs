string[] data = File.ReadAllLines("input.txt");

Dictionary<string, char> rules = data.Skip(2).Select(x => x.Split(" -> ")).ToDictionary(k => k.First(), v => v.Last().Single());

Dictionary<string, long> template = new();
for(int i = 0; i < data[0].Length - 1; i++)
{
    string pair = data[0].Substring(i, 2);
    if(!template.ContainsKey(pair))
        template.Add(pair, 0);
    template[pair]++;
}

Dictionary<char, long> elementCount = new();
for (int i = 0; i < data[0].Length; i++)
{
    if (!elementCount.ContainsKey(data[0][i]))
        elementCount.Add(data[0][i], 0);
    elementCount[data[0][i]]++;
}

for (int i = 0; i < 40; i++)
{
    Dictionary<string, long> templateUpdate = new();
    foreach(var rule in rules)
    {
        if (template.ContainsKey(rule.Key))
        {
            long currentCount = template[rule.Key];
            template.Remove(rule.Key);

            string pair1 = new string(new char[] { rule.Key[0], rule.Value });
            ProcessPair(template, templateUpdate, pair1, currentCount);

            string pair2 = new string(new char[] { rule.Value, rule.Key[1] });
            ProcessPair(template, templateUpdate, pair2, currentCount);

            if(!elementCount.ContainsKey(rule.Value))
                elementCount.Add(rule.Value, 0);
            elementCount[rule.Value] += currentCount;
        }
    }

    foreach (var update in templateUpdate)
    {
        if(!template.ContainsKey(update.Key))
            template.Add(update.Key, 0);

        template[update.Key] += update.Value;
    }
}

void ProcessPair(Dictionary<string, long> template, Dictionary<string, long> templateUpdate, string pair, long currentCount)
{
    if(templateUpdate.ContainsKey(pair))
        templateUpdate[pair] += currentCount;
    else
        templateUpdate.Add(pair, currentCount);
}

long[] elementCountOrder = elementCount.Select(x => x.Value).OrderBy(x => x).ToArray();
Console.WriteLine($"element calc: {elementCountOrder.Last() - elementCountOrder.First()}");