using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Point : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement e)
    {
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
    }
}
