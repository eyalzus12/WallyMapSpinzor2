using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class MovingPlatform : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public Animation Animation{get; set;} = null!;
    public List<Platform> Platforms{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        PlatID = element.GetAttribute("PlatID");
        //Animation is always supposed to exist
        //The game technically supports it not existing
        //In which case the moving platform doesn't exist
        Animation = element.DeserializeChildOfType<Animation>()!;
        Platforms = element.DeserializeChildrenOfType<Platform>();
    }

    public XElement Serialize()
    {
        XElement e = new("MovingPlatform");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);
        e.Add(Animation.Serialize());
        foreach(Platform p in Platforms)
            e.Add(p.Serialize());

        return e;
    }
}