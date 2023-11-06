using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public List<AbstractCollision> Collisions{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        PlatID = element.GetAttribute("PlatID");
        Collisions = element.DeserializeCollisionChildren();
    }

    public XElement Serialize()
    {
        XElement e = new("DynamicCollision");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);
        
        foreach(AbstractCollision c in Collisions)
            e.Add(c.Serialize());

        return e;
    }
}