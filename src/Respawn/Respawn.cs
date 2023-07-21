using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Respawn : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public bool Initial{get; set;}
    public bool ExpandedInit{get; set;}
    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        Initial = element.GetBoolAttribute("Initial", false);
        ExpandedInit = element.GetBoolAttribute("ExpandedInit", false);
    }
}