using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class MovingPlatform : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = "";
    public Animation? Animation{get; set;}
    public List<Platform> Platforms{get; set;} = new();

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        PlatID = element.GetAttribute("PlatID");
        Animation = element.DeserializeChildOfType<Animation>();
        Platforms = element.DeserializeChildrenOfType<Platform>();
    }
}