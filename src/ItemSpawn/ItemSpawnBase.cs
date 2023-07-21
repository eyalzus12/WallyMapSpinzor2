using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class ItemSpawnBase: IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 1.79769313486231e+308);
        Y = element.GetFloatAttribute("Y", 1.79769313486231e+308);
        W = element.GetFloatAttribute("W", DefaultW);
        H = element.GetFloatAttribute("H", DefaultH);
    }

    public virtual double DefaultW => 1.79769313486231e+308;
    public virtual double DefaultH => 10;
}