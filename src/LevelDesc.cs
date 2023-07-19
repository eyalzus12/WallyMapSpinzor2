using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class LevelDesc
{
    [XmlAttribute]
    public string? AssetDir{get; set;}

    [XmlAttribute]
    public string? LevelName{get; set;}

    [XmlIgnore]
    public int? NumFrames{get; set;}
    [XmlAttribute(nameof(NumFrames))]
    public string? _NumFrames
    {
        get => NumFrames.ToString();
        set => NumFrames = Utils.ParseIntOrNull(value);
    }

    [XmlIgnore]
    public float? SlowMult{get; set;}
    [XmlAttribute(nameof(SlowMult))]
    public string? _SlowMult
    {
        get => SlowMult.ToString();
        set => SlowMult = Utils.ParseFloatOrNull(value);
    }


    [XmlElement]
    public CameraBounds? CameraBounds{get; set;}

    [XmlElement]
    public SpawnBotBounds? SpawnBotBounds{get; set;}

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


    [XmlElement(nameof(Respawn))]
    public Respawn[]? RespawnList{get; set;}


    [XmlChoiceIdentifier(nameof(ItemSpawnTypeList))]
    [XmlElement(typeof(ItemSpawn))]
    [XmlElement(typeof(ItemInitSpawn))]
    [XmlElement(typeof(ItemSet))]
    [XmlElement(typeof(TeamItemInitSpawn))]
    public ItemSpawnBase[]? ItemSpawnList{get; set;}

    [XmlIgnore]
    public ItemSpawnBase.ItemSpawnType[]? ItemSpawnTypeList{get; set;}

    [XmlElement(nameof(NavNode))]
    public NavNode[]? NavNodeList{get; set;}

    [XmlElement(nameof(DynamicCollision))]
    public DynamicCollision[]? DynamicCollisionList{get; set;}

    [XmlElement(nameof(DynamicItemSpawn))]
    public DynamicItemSpawn[]? DynamicItemSpawnList{get; set;}

    [XmlElement(nameof(DynamicRespawn))]
    public DynamicRespawn[]? DynamicRespawnList{get; set;}

    [XmlElement(nameof(DynamicNavNode))]
    public DynamicNavNode[]? DynamicNavNodeList{get; set;}
}