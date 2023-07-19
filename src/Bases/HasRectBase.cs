using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class HasRectBase
{
    [XmlIgnore]
    public Rect Rect{get; set;} = Rect.ZERO;

    [XmlAttribute("X")]
    public string? _X
    {
        get => Rect.X.ToString();
        set => Rect = Rect with {X = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Y")]
    public string? _Y
    {
        get => Rect.Y.ToString();
        set => Rect = Rect with {Y = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("W")]
    public string? _W
    {
        get => Rect.W.ToString();
        set => Rect = Rect with {W = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("H")]
    public string? _H
    {
        get => Rect.H.ToString();
        set => Rect = Rect with {H = Utils.ParseFloatOrNull(value)??0};
    }
}