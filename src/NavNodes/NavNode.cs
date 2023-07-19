using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class NavNode : HasPositionBase
{
    [XmlIgnore]
    public NavNodeData Data{get; set;}

    [XmlAttribute]
    public string? NavID
    {
        get => Data.NavID.ToString();
        set => Data = (value is null)?new():Utils.ParseNavNodeData(value);
    }

    [XmlIgnore]
    public NavNodeData[] Path{get; set;} = {};

    [XmlAttribute(nameof(Path))]
    public string? _Path
    {
        get => string.Join(',', Path);
        set => Path = value?.Split(',').Select(s => Utils.ParseNavNodeData(s)).ToArray() ?? new NavNodeData[]{};
    }
}