using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class PressurePlateCollisionBase : CollisionBase
{
    [XmlIgnore]
    public Position AnimOffset{get; set;} = Position.ZERO; //REMEMBER: this is not actually an offset

    [XmlAttribute("AnimOffsetX")]
    public string? _AnimOffsetX
    {
        get => AnimOffset.X.ToString();
        set => AnimOffset = AnimOffset with {X = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("AnimOffsetY")]
    public string? _AnimOffsetY
    {
        get => AnimOffset.Y.ToString();
        set => AnimOffset = AnimOffset with {Y = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlIgnore]
    public Position FireOffset{get; set;} = Position.ZERO; //REMEMBER: this is not actually an offset

    [XmlAttribute("FireOffsetX")]
    public string? _FireOffsetX
    {
        get => FireOffset.X.ToString();
        set => FireOffset = FireOffset with {X = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("FireOffsetY")]
    public string? _FireOffsetY
    {
        get => FireOffset.Y.ToString();
        set => FireOffset = FireOffset with {Y = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlIgnore]
    public int Cooldown{get; set;} = 0;//TODO: check what the actual default is

    [XmlAttribute(nameof(Cooldown))]
    public string? _Cooldown
    {
        get => Cooldown.ToString();
        set => Cooldown = Utils.ParseIntOrNull(value) ?? 0;
    }

    [XmlIgnore]
    public bool FaceLeft{get; set;} = false;
    [XmlAttribute(nameof(FaceLeft))]
    public string? _FaceLeft
    {
        get => FaceLeft.ToString();
        set => FaceLeft = Utils.ParseBoolOrNull(value) ?? false;
    }

    [XmlAttribute]
    public string? AssetName{get; set;}

    [XmlAttribute]
    public string? TrapPowers{get; set;}

    [XmlIgnore]
    public int? PlatID{get; set;}
    [XmlAttribute(nameof(PlatID))]
    public string? _PlatID
    {
        get => PlatID.ToString();
        set => PlatID = Utils.ParseIntOrNull(value);
    }
}