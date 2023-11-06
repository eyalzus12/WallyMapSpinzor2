using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Platform : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double ScaleX{get; set;}
    public double ScaleY{get; set;}
    public double Rotation{get; set;}
    public string? AssetName{get; set;}
    public string? ScoringType{get; set;}
    public string? PlatformAssetSwap{get; set;}
    public List<string>? Theme{get; set;}
    public string InstanceName{get; set;} = null!;

    public List<Platform> Platforms{get; set;} = null!;
    public List<Asset> Assets{get; set;} = null!;
    
    public bool NoSkulls => InstanceName == "am_NoSkulls";
    public string? Hotkey => InstanceName.StartsWith("am_Hotkey")?InstanceName.Substring(InstanceName.LastIndexOf('_')+1):null;
    public int? Blue => InstanceName.StartsWith("am_Blue")?int.Parse(InstanceName.Substring("am_Blue".Length)):null;
    public int? Red => InstanceName.StartsWith("am_Red")?int.Parse(InstanceName.Substring("am_Red".Length)):null;

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        double Scale = element.GetFloatAttribute("Scale", 1);
        ScaleX = element.GetFloatAttribute("ScaleX", Scale);
        ScaleY = element.GetFloatAttribute("ScaleY", Scale);
        Rotation = element.GetFloatAttribute("Rotation", 0);
        AssetName = element.GetNullableAttribute("AssetName");
        ScoringType = element.GetNullableAttribute("ScoringType");
        PlatformAssetSwap = element.GetNullableAttribute("PlatformAssetSwap");
        Theme = element.GetNullableAttribute("Theme")?.Split(',').ToList();
        InstanceName = element.GetAttribute("InstanceName");
        Platforms = element.DeserializeChildrenOfType<Platform>();
        Assets = element.DeserializeChildrenOfType<Asset>();
    }

    public XElement Serialize()
    {
        XElement e = new("Platform");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        if(ScaleX == ScaleY)
        {
            e.SetAttributeValue("Scale", ScaleX.ToString());
        }
        else
        {
            e.SetAttributeValue("ScaleX", ScaleX.ToString());
            e.SetAttributeValue("ScaleY", ScaleY.ToString());
        }

        if(Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());

        if(AssetName is not null)
            e.SetAttributeValue("AssetName", AssetName);
        
        if(ScoringType is not null)
            e.SetAttributeValue("ScoringType", ScoringType);
        
        if(PlatformAssetSwap is not null)
            e.SetAttributeValue("PlatformAssetSwap", PlatformAssetSwap);
        
        if(Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));

        e.SetAttributeValue("InstanceName", InstanceName);

        foreach(Platform p in Platforms)
            e.Add(p.Serialize());
        
        foreach(Asset a in Assets)
            e.Add(a.Serialize());

        return e;
    }
}