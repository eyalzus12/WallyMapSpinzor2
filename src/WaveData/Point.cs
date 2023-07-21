using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Point : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
    }
}