using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicNavNode : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public List<NavNode> NavNodes{get; set;} = null!;

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        PlatID = element.GetAttribute("PlatID");
        NavNodes = element.DeserializeChildrenOfType<NavNode>();
    }

    public XElement Serialize()
    {
        XElement e = new("DynamicNavNode");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);

        foreach(NavNode n in NavNodes)
            e.Add(n.Serialize());

        return e;
    }
}