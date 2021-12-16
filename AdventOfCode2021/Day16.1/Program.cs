using System.Collections;
using System.Globalization;

string hex = File.ReadAllText("input.txt");

Queue<bool> bits = new Queue<bool>(4 * hex.Length);
for (int i = 0; i < hex.Length; i++)
{
    byte b = byte.Parse(hex[i].ToString(), NumberStyles.HexNumber);
    for (int j = 0; j < 4; j++)
        bits.Enqueue((b & (1 << (3 - j))) != 0);
}

List<Packet> packets = new();
while (bits.Count > 7) // we need atleast 7 or more bits to parse a packet
    packets.Add(ParsePacket());

Packet ParsePacket()
{
    int version = bits.DequeueBits(3).ToInt32();
    int typeId = bits.DequeueBits(3).ToInt32();

    if (typeId == 4)
    {
        bool foundEnd = false;
        List<bool> literalBits = new();
        do
        {
            foundEnd = !bits.Dequeue();
            for (int i = 0; i < 4; i++)
                literalBits.Insert(0, bits.Dequeue());
        } while (!foundEnd);

        int literal = new BitArray(literalBits.ToArray()).ToInt32();

        var packet = new LiteralPacket(version, typeId, literal);
        packet.Size = 6 + literalBits.Count + literalBits.Count / 4;
        return packet;
    }
    else
    {
        OperatorPacket packet = new(version, typeId);

        if (bits.Dequeue())
        {
            // read 11 bits to see how many sub packets to read
            int subPackets = bits.DequeueBits(11).ToInt32();
            while(subPackets > packet.SubPackets.Count)
                packet.SubPackets.Add(ParsePacket());

            packet.Size = 6 + 1 + 11 + packet.SubPackets.Sum(x => x.Size);
        }
        else
        {
            // read 15 bits to see how many bits for sub packets to read
            int subPacketsSize = bits.DequeueBits(15).ToInt32();
            while (subPacketsSize > packet.SubPackets.Sum(x => x.Size))
                packet.SubPackets.Add(ParsePacket());

            packet.Size = 6 + 1 + 15 + subPacketsSize;
        }

        return packet;
    }
}

Console.WriteLine($"version sum: {packets.Sum(x => x.TotalVersionSum())}");

public static class Extensions {
    public static BitArray DequeueBits(this Queue<bool> bits, int size)
    {
        BitArray data = new BitArray(size);
        for (int i = 0; i < size; i++)
            data.Set(size - 1 - i, bits.Dequeue());

        return data;
    }

    public static int ToInt32(this BitArray bits)
    {
        int[] array = new int[4];
        bits.CopyTo(array, 0);
        return array[0];
    }
}
class OperatorPacket : Packet
{
    public List<Packet> SubPackets { get; }

    public OperatorPacket(int version, int typeId) : base(version, typeId)
    {
        SubPackets = new List<Packet>();
    }

    public override int TotalVersionSum()
    {
        return base.TotalVersionSum() + SubPackets.Sum(x => x.TotalVersionSum());
    }
}

class LiteralPacket : Packet
{
    public int Literal { get; }

    public LiteralPacket(int version, int typeId, int literal) : base(version, typeId)
    {
        Literal = literal;
    }
}

abstract class Packet
{
    public int Version { get; }
    public int TypeId { get; }
    public int Size { get; set; }

    public Packet(int version, int typeId)
    {
        Version = version;
        TypeId = typeId;
    }

    public virtual int TotalVersionSum()
    {
        return Version;
    }
}