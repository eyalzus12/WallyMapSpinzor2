using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class AnimatedBackground : IDeserializable, ISerializable
{
    public bool Midground{get; set;}

    public Gfx Gfx{get; set;} = null!;

    public double Position_X{get; set;}
    public double Position_Y{get; set;}

    public double Rotation{get; set;}

    public double Skew_X{get; set;}
    public double Skew_Y{get; set;}

    public double Scale_X{get; set;}
    public double Scale_Y{get; set;}

    public int FrameOffset{get; set;}

    public void Deserialize(XElement element)
    {
        Midground = element.GetBoolAttribute("Midground", false);

        Gfx = element.DeserializeChildOfType<Gfx>()!;

        string[]? Position = element.Element("Position")?.Value.Split(',', 2);
        Position_X = Utils.ParseFloatOrNull(Position?[0]) ?? 0;
        Position_Y = Utils.ParseFloatOrNull(Position?[1]) ?? 0;

        Rotation = Utils.ParseFloatOrNull(element.Element("Rotation")?.Value) ?? 0;
        
        string[]? Skew = element.Element("Skew")?.Value.Split(',', 2);
        Skew_X = Utils.ParseFloatOrNull(Skew?[0]) ?? 0;
        Skew_Y = Utils.ParseFloatOrNull(Skew?[1]) ?? 0;

        string[]? Scale = element.Element("Scale")?.Value.Split(',', 2);
        Scale_X = Utils.ParseFloatOrNull(Scale?[0]) ?? 1;
        Scale_Y = Utils.ParseFloatOrNull(Scale?[1]) ?? 1;

        FrameOffset = Utils.ParseIntOrNull(element.Element("FrameOffset")?.Value) ?? 0;
    }

    public XElement Serialize()
    {
        XElement e = new("AnimatedBackground");

        if(Midground)
            e.SetAttributeValue("Midground", Midground.ToString().ToLower());
        
        e.Add(Gfx.Serialize());

        e.Add(new XElement("Position", $"{Position_X},{Position_Y}"));
        
        if(Rotation != 0)
            e.Add(new XElement("Rotation", Rotation.ToString()));
        
        if(Rotation == 0 || Skew_X != 0 || Skew_Y != 0)
            e.Add(new XElement("Skew", $"{Skew_X},{Skew_Y}"));

        e.Add(new XElement("Scale", $"{Scale_X},{Scale_Y}"));

        if(FrameOffset != 0)
            e.Add(new XElement("FrameOffset", FrameOffset));

        return e;
    }
}