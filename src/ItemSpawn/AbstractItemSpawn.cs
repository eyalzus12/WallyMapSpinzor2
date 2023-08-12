using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractItemSpawn: IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", DefaultX);
        Y = element.GetFloatAttribute("Y", DefaultY);
        W = element.GetFloatAttribute("W", DefaultW);
        H = element.GetFloatAttribute("H", DefaultH);
    }

    public abstract double DefaultX{get;}
    public abstract double DefaultY{get;}
    public abstract double DefaultW{get;}
    public abstract double DefaultH{get;}
}