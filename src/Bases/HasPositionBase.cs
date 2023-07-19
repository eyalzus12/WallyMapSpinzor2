using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class HasPositionBase
{
    [XmlIgnore]
    public Position Position{get; set;} = Position.ZERO;

    [XmlAttribute("X")]
    public string? _X
    {
        get => Position.X.ToString();
        set => Position = Position with {X = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Y")]
    public string? _Y
    {
        get => Position.Y.ToString();
        set => Position = Position with {Y = Utils.ParseFloatOrNull(value)??0};
    }
}