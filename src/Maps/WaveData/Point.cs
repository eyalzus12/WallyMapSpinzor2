using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Point : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
    }

    public XElement Serialize()
    {
        XElement e = new("Point");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        return e;
    }
}