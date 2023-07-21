using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class MovingPlatform : HasPositionBase
{
    [XmlIgnore]
    public int? PlatID{get; set;}
    [XmlAttribute(nameof(PlatID))]
    public string? _PlatID
    {
        get => PlatID.ToString();
        set => PlatID = Utils.ParseIntOrNull(value);
    }

    [XmlElement(nameof(Platform))]
    public Platform[]? PlatformList{get; set;}
}