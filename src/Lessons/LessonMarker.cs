using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonMarker : IDeserializable, ISerializable
{
    private static (double, double) DefaultDimensions(string type) => type switch
    {
        "Trigger" => (120, 120),
        "Waypoint" => (250, 250),
        _ => throw new SerializationException($"Invalid marker type {type}"),
    };

    public uint OrderID { get; set; }
    public string Type { get; set; } = null!;
    public double Position_X { get; set; }
    public double Position_Y { get; set; }
    public uint TriggerDuration { get; set; }
    public double Dimensions_W { get; set; }
    public double Dimensions_H { get; set; }

    public void Deserialize(XElement e)
    {
        OrderID = e.GetUIntAttribute("OrderID");
        Type = e.GetAttribute("Type");

        string[]? Position = e.GetElement("Position")?.Split(',');
        if (Position is not null)
        {
            if (Position.Length != 2) throw new SerializationException($"LessonMarker element {e} has invalid Position");
            Position_X = double.Parse(Position[0]);
            Position_Y = double.Parse(Position[1]);
        }
        else
        {
            Position_X = Position_Y = 0;
        }

        TriggerDuration = e.GetUIntElement("TriggerDuration", 0);

        string[]? Dimensions = e.GetElementOrNull("Dimensions")?.Split(',');
        if (Dimensions is not null)
        {
            if (Dimensions.Length != 2) throw new SerializationException($"LessonMarker element {e} has invalid Dimensions");
            Dimensions_W = double.Parse(Dimensions[0]);
            Dimensions_H = double.Parse(Dimensions[1]);
        }
        else
        {
            (Dimensions_W, Dimensions_H) = DefaultDimensions(Type);
        }
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("OrderID", OrderID);
        e.SetAttributeValue("Type", Type);
        e.AddChild("Position", $"{Position_X},{Position_Y}");
        if (TriggerDuration != 0) e.AddChild("TriggerDuration", TriggerDuration);
        e.AddChild("Dimensions", $"{Dimensions_W},{Dimensions_H}");
    }
}