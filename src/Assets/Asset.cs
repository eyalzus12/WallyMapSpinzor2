using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Asset : IDeserializable
{
    //Assets seem to be capable of defining scale and rotation
    //but this is never used
    public string AssetName{get; set;} = "";
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}
    public virtual void Deserialize(XElement element)
    {
        AssetName = element.GetAttribute("AssetName");
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        H = element.GetFloatAttribute("H", 0);
        W = element.GetFloatAttribute("W", 0);
    }
}