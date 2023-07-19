using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class DynamicNavNode
{
    [XmlIgnore]
    public int? PlatID{get; set;}
    [XmlAttribute(nameof(PlatID))]
    public string? _PlatID
    {
        get => PlatID.ToString();
        set => PlatID = Utils.ParseIntOrNull(value);
    }

    [XmlElement(nameof(NavNode))]
    public NavNode[]? NavNodeList{get; set;}
}