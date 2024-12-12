using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonItem : IDeserializable, ISerializable
{
    public string ItemType { get; set; } = null!;
    public double Position_X { get; set; }
    public double Position_Y { get; set; }

    public void Deserialize(XElement e)
    {
        ItemType = e.GetAttribute("ItemType");

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
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("ItemType", ItemType);
        e.AddChild("Position", $"{Position_X},{Position_Y}");
    }
}