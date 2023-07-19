using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class DynamicItemSpawn : HasPositionBase
{
    [XmlIgnore]
    public int? PlatID{get; set;}
    [XmlAttribute(nameof(PlatID))]
    public string? _PlatID
    {
        get => PlatID.ToString();
        set => PlatID = Utils.ParseIntOrNull(value);
    }

    [XmlChoiceIdentifier(nameof(ItemSpawnTypeList))]
    [XmlElement(typeof(ItemSpawn))]
    [XmlElement(typeof(ItemInitSpawn))]
    [XmlElement(typeof(ItemSet))]
    [XmlElement(typeof(TeamItemInitSpawn))]
    public ItemSpawnBase[]? ItemSpawnList{get; set;}

    [XmlIgnore]
    public ItemSpawnBase.ItemSpawnType[]? ItemSpawnTypeList{get; set;}
}