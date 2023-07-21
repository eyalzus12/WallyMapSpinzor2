using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Asset : IDeserializable
{
    public string AssetName{get; set;} = "";
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}
    public double ScaleX{get; set;}
    public double ScaleY{get; set;}
    public double Rotation{get; set;}

    public virtual void Deserialize(XElement element)
    {
        AssetName = element.GetAttribute("AssetName");
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        H = element.GetFloatAttribute("H", 0);
        W = element.GetFloatAttribute("W", 0);
        double Scale = element.GetFloatAttribute("Scale", 1);
        ScaleX = element.GetFloatAttribute("ScaleX", Scale);
        ScaleY = element.GetFloatAttribute("ScaleY", Scale);
        Rotation = Utils.DegToRad(element.GetFloatAttribute("Rotation", 0));
    }
}