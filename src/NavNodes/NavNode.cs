using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class NavNode : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string NavID{get; set;} = null!;
    public string[] Path{get; set;} = null!;

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        NavID = element.GetAttribute("NavID");
        Path = element.GetAttribute("Path").Split(',');
    }
}