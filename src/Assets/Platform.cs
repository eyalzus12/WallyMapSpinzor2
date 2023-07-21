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
    }



    /*
    [XmlAttribute]
    public string? AssetName;

    [XmlIgnore]
    public float? W{get; set;}
    [XmlAttribute(nameof(W))]
    public string? _W
    {
        get => W.ToString();
        set => W = Utils.ParseFloatOrNull(value);
    }

    [XmlIgnore]
    public float? H{get; set;}
    [XmlAttribute(nameof(H))]
    public string? _H
    {
        get => H.ToString();
        set => H = Utils.ParseFloatOrNull(value);
    }

    [XmlAttribute]
    public string? InstanceName{get; set;}

    [XmlAttribute]
    public string? ScoringType{get; set;}

    [XmlIgnore]
    public string[]? Theme{get; set;}
    [XmlAttribute(nameof(Theme))]
    public string? _Theme
    {
        get => (Theme is null)?null:string.Join(',', Theme);
        set => Theme = value?.Split(',').ToArray();
    }

    [XmlElement(nameof(Asset))]
    public Asset[]? AssetList{get; set;}

    [XmlElement(nameof(Platform))]
    public Platform[]? PlatformList{get; set;}
    */
}