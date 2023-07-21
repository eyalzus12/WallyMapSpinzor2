using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = "";
    public List<CollisionBase> Collisions{get; set;} = new();

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        PlatID = element.GetAttribute("PlatID");
        Collisions = element.DeserializeCollisionChildren();
    }
}