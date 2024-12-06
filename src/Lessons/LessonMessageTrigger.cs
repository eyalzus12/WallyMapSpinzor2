using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonMessageTrigger : IDeserializable, ISerializable
{
    public double Position_X { get; set; }
    public double Position_Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public uint TimeBetweenTriggers { get; set; }
    public uint Timeout { get; set; }
    public uint NumFailsTrigger { get; set; }
    public uint Waypoint { get; set; }
    public string? PowerDataFailedRatio { get; set; } // format x,x:x
    public string MessageKey { get; set; } = null!;

    // not in template.
    public string? DevNote { get; set; }
    public string? MessagePosition { get; set; } // wtf?

    public void Deserialize(XElement e)
    {
        string[]? Position = e.GetElement("Position")?.Split(',');
        if (Position is not null)
        {
            if (Position.Length != 2) throw new SerializationException($"LessonMessageTrigger element {e} has invalid Position");
            Position_X = double.Parse(Position[0]);
            Position_Y = double.Parse(Position[1]);
        }
        else
        {
            Position_X = Position_Y = 0;
        }

        Width = e.GetDoubleElement("Width", 0);
        Height = e.GetDoubleElement("Height", 0);
        TimeBetweenTriggers = e.GetUIntElement("TimeBetweenTriggers", 0);
        Timeout = e.GetUIntElement("Timeout", 0);
        NumFailsTrigger = e.GetUIntElement("NumFailsTrigger");
        Waypoint = e.GetUIntElement("Waypoint");
        PowerDataFailedRatio = e.GetElementOrNull("PowerDataFailedRatio");
        MessageKey = e.GetElement("MessageKey");

        DevNote = e.GetElementOrNull("DevNote");
        MessagePosition = e.GetElementOrNull("MessagePosition");
    }

    public void Serialize(XElement e)
    {
        throw new System.NotImplementedException();
    }
}