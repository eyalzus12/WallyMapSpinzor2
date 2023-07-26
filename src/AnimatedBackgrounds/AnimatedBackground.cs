using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class AnimatedBackground : IDeserializable
{
    public int FrameOffset{get; set;}

    public double Position_X{get; set;}
    public double Position_Y{get; set;}

    public double Rotation{get; set;}

    public double Scale_X{get; set;}
    public double Scale_Y{get; set;}

    public double Skew_X{get; set;}
    public double Skew_Y{get; set;}

    public Gfx? Gfx{get; set;}


    public void Deserialize(XElement element)
    {
        FrameOffset = Utils.ParseIntOrNull(element.Element("FrameOffset")?.Value) ?? 0;
        string[]? Position = element.Element("Position")?.Value.Split(',', 2);
        Position_X = Utils.ParseFloatOrNull(Position?[0]) ?? 0;
        Position_Y = Utils.ParseFloatOrNull(Position?[1]) ?? 0;
        Rotation = Utils.DegToRad(Utils.ParseFloatOrNull(element.Element("Rotation")?.Value) ?? 0);
        string[]? Scale = element.Element("Scale")?.Value.Split(',', 2);
        Scale_X = Utils.ParseFloatOrNull(Scale?[0]) ?? 1;
        Scale_Y = Utils.ParseFloatOrNull(Scale?[1]) ?? 1;
        string[]? Skew = element.Element("Skew")?.Value.Split(',', 2);
        Skew_X = Utils.DegToRad(Utils.ParseFloatOrNull(Skew?[0]) ?? 0);
        Skew_Y = Utils.DegToRad(Utils.ParseFloatOrNull(Skew?[1]) ?? 0);
        Gfx = element.DeserializeChildOfType<Gfx>();
    }
}