using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class KeyFrame : HasPositionBase
{
    [XmlIgnore]
    public int FrameNum{get; set;}
    [XmlAttribute(nameof(FrameNum))]
    public string? _FrameNum
    {
        get => FrameNum.ToString();
        set => FrameNum = Utils.ParseIntOrNull(value) ?? 0;
    }
}