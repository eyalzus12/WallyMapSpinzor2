using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Platform : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double ScaleX{get; set;}
    public double ScaleY{get; set;}
    public double Rotation{get; set;}
    public string? AssetName{get; set;}
    public string? ScoringType{get; set;}
    public string[]? Theme{get; set;}
    public string InstanceName{get; set;} = "";

    public List<Platform> Platforms{get; set;} = new();
    public List<Asset> Assets{get; set;} = new();
    
    public bool NoSkulls{get; set;}
    public string? Hotkey{get; set;}
    public int? Blue{get; set;}
    public int? Red{get; set;}

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        double Scale = element.GetFloatAttribute("Scale", 1);
        ScaleX = element.GetFloatAttribute("ScaleX", Scale);
        ScaleY = element.GetFloatAttribute("ScaleY", Scale);
        Rotation = Utils.DegToRad(element.GetFloatAttribute("Rotation", 0));
        AssetName = element.GetNullableAttribute("AssetName");
        ScoringType = element.GetNullableAttribute("ScoringType");
        Theme = element.GetNullableAttribute("Theme")?.Split(',');
        InstanceName = element.GetAttribute("InstanceName");
        Platforms = element.DeserializeChildrenOfType<Platform>();
        Assets = element.DeserializeChildrenOfType<Asset>();

        NoSkulls = InstanceName == "am_NoSkulls";
        Hotkey = InstanceName.StartsWith("am_Hotkey")?InstanceName.Substring(InstanceName.LastIndexOf('_')+1):null;
        Blue = InstanceName.StartsWith("am_Blue")?int.Parse(InstanceName.Substring("am_Blue".Length)):null;
        Red = InstanceName.StartsWith("am_Red")?int.Parse(InstanceName.Substring("am_Red".Length)):null;
    }
}