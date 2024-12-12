using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonWorldHotkey : IDeserializable, ISerializable
{
    public double Position_X { get; set; }
    public double Position_Y { get; set; }
    public uint[] Commands { get; set; } = [];

    public void Deserialize(XElement e)
    {
        string[]? Position = e.GetElement("Position")?.Split(',');
        if (Position is not null)
        {
            if (Position.Length != 2) throw new SerializationException($"LessonWorldHotkey element {e} has invalid Position");
            Position_X = double.Parse(Position[0]);
            Position_Y = double.Parse(Position[1]);
        }
        else
        {
            Position_X = Position_Y = 0;
        }

        Commands = e.GetElementOrNull("Commands")?.Split(',').Select(uint.Parse).ToArray() ?? [];
    }

    public void Serialize(XElement e)
    {
        e.AddChild("Position", $"{Position_X},{Position_Y}");
        if (Commands.Length > 0)
            e.AddChild("Commands", string.Join(',', Commands));
    }
}