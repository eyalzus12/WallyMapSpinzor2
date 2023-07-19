using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class HasLineBase
{
    [XmlIgnore]
    public Line Line{get; set;} = Line.ZERO;

    [XmlAttribute("X")]
    public string? _X {get => null; set {_X1 = value; _X2 = value;}}

    [XmlAttribute("Y")]
    public string? _Y {get => null; set {_Y1 = value; _Y2 = value;}}

    [XmlAttribute("X1")]
    public string? _X1
    {
        get => Line.X1.ToString();
        set => Line = Line with {X1 = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("X2")]
    public string? _X2
    {
        get => Line.X2.ToString();
        set => Line = Line with {X2 = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Y1")]
    public string? _Y1
    {
        get => Line.Y1.ToString();
        set => Line = Line with {Y1 = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Y2")]
    public string? _Y2
    {
        get => Line.Y2.ToString();
        set => Line = Line with {Y2 = Utils.ParseFloatOrNull(value)??0};
    }
}