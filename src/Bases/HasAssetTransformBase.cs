using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class HasAssetTransformBase
{
    [XmlIgnore]
    public AssetTransform Transform{get; set;} = AssetTransform.IDENTITY;

    [XmlAttribute("X")]
    public string? _X
    {
        get => Transform.X.ToString();
        set => Transform = Transform with {X = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Y")]
    public string? _Y
    {
        get => Transform.Y.ToString();
        set => Transform = Transform with {Y = Utils.ParseFloatOrNull(value)??0};
    }

    [XmlAttribute("Scale")]
    public string? _Scale
    {
        get => null;
        set {_ScaleX = value; _ScaleY = value;}
    }

    [XmlAttribute("ScaleX")]
    public string? _ScaleX
    {
        get => Transform.ScaleX.ToString();
        set => Transform = Transform with {ScaleX = Utils.ParseFloatOrNull(value)??1};
    }

    [XmlAttribute("ScaleY")]
    public string? _ScaleY
    {
        get => Transform.ScaleY.ToString();
        set => Transform = Transform with {ScaleY = Utils.ParseFloatOrNull(value)??1};
    }

    [XmlAttribute("Rotation")]
    public string? _Rotation
    {
        get => Transform.Rotation.ToString();
        set => Transform = Transform with {Rotation = Utils.ParseFloatOrNull(value)??1};
    }
}