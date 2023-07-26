using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Background : IDeserializable
{
    public double W{get; set;}
    public double H{get; set;}
    public string AssetName{get; set;} = "";
    public bool HasSkulls{get; set;}
    public string[]? Theme{get; set;}

    public void Deserialize(XElement element)
    {
        W = element.GetFloatAttribute("W");
        H = element.GetFloatAttribute("H");
        AssetName = element.GetAttribute("AssetName");
        HasSkulls = element.GetBoolAttribute("HasSkulls", false);
        Theme = element.GetNullableAttribute("Theme")?.Split(",");
    }
}