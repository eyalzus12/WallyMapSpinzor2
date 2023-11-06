using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Respawn : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public bool Initial{get; set;}
    public bool ExpandedInit{get; set;}
    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        Initial = element.GetBoolAttribute("Initial", false);
        ExpandedInit = element.GetBoolAttribute("ExpandedInit", false);
    }

    public XElement Serialize()
    {
        XElement e = new("Respawn");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        if(Initial)
            e.SetAttributeValue("Initial", Initial);
        if(ExpandedInit)
            e.SetAttributeValue("ExpandedInit", ExpandedInit);

        return e;
    }
}