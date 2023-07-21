using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class Platform : HasAssetTransformBase
{
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
}