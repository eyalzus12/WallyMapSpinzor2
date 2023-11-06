using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class NavNode : IDeserializable, ISerializable
{
    public string NavID{get; set;} = null!;
    public List<string> Path{get; set;} = null!;
    public double X{get; set;}
    public double Y{get; set;}

    public void Deserialize(XElement element)
    {
        NavID = element.GetAttribute("NavID");
        Path = element.GetAttribute("Path").Split(',').ToList();
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
    }

    public XElement Serialize()
    {
        XElement e = new("NavNode");

        e.SetAttributeValue("NavID", NavID);
        e.SetAttributeValue("Path", string.Join(',', Path));
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        return e;
    }
}