using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Asset : IDeserializable, ISerializable
{
    //Assets seem to be capable of defining scale and rotation
    //but this is never used
    public string AssetName{get; set;} = null!;
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

    public XElement Serialize()
    {
        XElement e = new("Asset");

        e.SetAttributeValue("AssetName", AssetName);
        if(X != 0)
            e.SetAttributeValue("X", X.ToString());
        if(Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("W", W.ToString());

        return e;
    }
}