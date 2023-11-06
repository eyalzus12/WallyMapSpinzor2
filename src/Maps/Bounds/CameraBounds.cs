using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CameraBounds : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}
    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        W = element.GetFloatAttribute("W", 0);
        H = element.GetFloatAttribute("H", 0);
    }

    public XElement Serialize()
    {
        XElement e = new("CameraBounds");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("H", H.ToString());

        return e;
    }
}