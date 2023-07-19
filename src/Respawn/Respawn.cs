using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class Respawn : HasPositionBase
{
    [XmlIgnore]
    public bool Initial{get; set;} = false;
    [XmlAttribute(nameof(Initial))]
    public string? _Initial
    {
        get => Initial.ToString();
        set => Initial = Utils.ParseBoolOrNull(value)??false;
    }

    [XmlIgnore]
    public bool ExpandedInit{get; set;} = false;
    [XmlAttribute(nameof(ExpandedInit))]
    public string? _ExpandedInit
    {
        get => ExpandedInit.ToString();
        set => ExpandedInit = Utils.ParseBoolOrNull(value)??false;
    }
}