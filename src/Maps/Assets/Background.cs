using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Background : IDeserializable, ISerializable
{
    public double W{get; set;}
    public double H{get; set;}
    //AssetName and AnimatedAssetName cannot both be non-null at the same time
    public string? AssetName{get; set;}
    public string? AnimatedAssetName{get; set;}
    public bool HasSkulls{get; set;}
    public List<string>? Theme{get; set;}

    public void Deserialize(XElement element)
    {
        W = element.GetFloatAttribute("W");
        H = element.GetFloatAttribute("H");
        AssetName = element.GetNullableAttribute("AssetName");
        AnimatedAssetName = element.GetNullableAttribute("AnimatedAssetName");
        HasSkulls = element.GetBoolAttribute("HasSkulls", false);
        Theme = element.GetNullableAttribute("Theme")?.Split(',').ToList();
    }

    public XElement Serialize()
    {
        XElement e = new("Background");

        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("AssetName", AssetName);
        if(AnimatedAssetName != null)
            e.SetAttributeValue("AnimatedAssetName", AnimatedAssetName);
        if(HasSkulls)
            e.SetAttributeValue("HasSkulls", HasSkulls.ToString());
        if(Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));

        return e;
    }
}