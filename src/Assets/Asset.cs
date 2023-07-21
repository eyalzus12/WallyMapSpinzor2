using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class Asset : HasRectBase
{
    [XmlAttribute]
    public string? AssetName{get; set;}
}