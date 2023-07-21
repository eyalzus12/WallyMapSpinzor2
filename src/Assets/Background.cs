using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class Background
{
    [XmlAttribute("W")]
    public string? _W{get; set;} //Doesn't matter for anything since it's parallax

    [XmlAttribute("H")]
    public string? _H{get; set;}

    [XmlAttribute]
    public string? AssetName{get; set;}

    [XmlIgnore]
    public bool HasSkulls{get; set;} = false;
    [XmlAttribute(nameof(HasSkulls))]
    public string? _HasSkulls
    {
        get => HasSkulls.ToString();
        set => HasSkulls = Utils.ParseBoolOrNull(value) ?? false;
    }
}