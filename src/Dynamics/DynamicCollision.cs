using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class DynamicCollision : HasPositionBase
{
    [XmlIgnore]
    public int? PlatID{get; set;}
    [XmlAttribute(nameof(PlatID))]
    public string? _PlatID
    {
        get => PlatID.ToString();
        set => PlatID = Utils.ParseIntOrNull(value);
    }

    [XmlChoiceIdentifier(nameof(CollisionTypeList))]
    [XmlElement(typeof(HardCollision))]
    [XmlElement(typeof(SoftCollision))]
    [XmlElement(typeof(NoSlideCollision))]
    [XmlElement(typeof(BouncyHardCollision))]
    [XmlElement(typeof(BouncySoftCollision))]
    [XmlElement(typeof(BouncyNoSlideCollision))]
    [XmlElement(typeof(GamemodeHardCollision))]
    [XmlElement(typeof(ItemIgnoreCollision))]
    [XmlElement(typeof(StickyCollision))]
    [XmlElement(typeof(TriggerCollision))]
    [XmlElement(typeof(PressurePlateCollision))]
    [XmlElement(typeof(SoftPressurePlateCollision))]
    public CollisionBase[]? CollisionList{get; set;}

    [XmlIgnore]
    public CollisionBase.CollisionType[]? CollisionTypeList{get; set;}
}