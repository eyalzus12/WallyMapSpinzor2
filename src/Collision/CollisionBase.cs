using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class CollisionBase : HasLineBase
{

    [XmlAttribute]
    public string? TauntEvent{get; set;}

    [XmlAttribute]
    public string? Flag{get; set;}

    [XmlAttribute]
    public string? ColorFlag{get; set;}

    [XmlIgnore]
    public Position? Anchor{get; set;}
    [XmlAttribute("AnchorX")]
    public string? _AnchorX
    {
        get => Anchor?.X.ToString();
        set => Anchor = new Position(Utils.ParseFloatOrNull(value) ?? 0, Anchor?.Y ?? 0);
    }
    [XmlAttribute("AnchorY")]
    public string? _AnchorY
    {
        get => Anchor?.Y.ToString();
        set => Anchor = new Position(Anchor?.X ?? 0, Utils.ParseFloatOrNull(value) ?? 0);
    }

    [XmlIgnore]
    public Position? Normal{get; set;}
    [XmlAttribute("NormalX")]
    public string? _NormalX
    {
        get => Normal?.X.ToString();
        set => Normal = new Position(Utils.ParseFloatOrNull(value) ?? 0, Normal?.Y ?? 0);
    }
    [XmlAttribute("NormalY")]
    public string? _NormalY
    {
        get => Normal?.Y.ToString();
        set => Normal = new Position(Normal?.X ?? 0, Utils.ParseFloatOrNull(value) ?? 0);
    }

    [XmlIgnore]
    public int? Team{get; set;}
    [XmlAttribute(nameof(Team))]
    public string? _Team{get => Team?.ToString(); set => Team = Utils.ParseIntOrNull(value);}


    [XmlType(IncludeInSchema=false)]
    public enum CollisionType
    {
        None,
        HardCollision,
        SoftCollision,
        NoSlideCollision,
        BouncyHardCollision,
        BouncySoftCollision,
        BouncyNoSlideCollision,
        GamemodeHardCollision,
        ItemIgnoreCollision,
        StickyCollision,
        TriggerCollision,
        PressurePlateCollision,
        SoftPressurePlateCollision
    }

    public virtual CollisionType Type => CollisionType.None;
}