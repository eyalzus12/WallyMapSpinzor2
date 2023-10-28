using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Animation : IDeserializable
{
    //these two can get a default from LevelDesc
    public double? SlowMult{get; set;}
    public int? NumFrames{get; set;}
    public int StartFrame{get; set;}
    public bool HasCenter{get; set;}
    public double CenterX{get; set;}
    public double CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int EasePower{get; set;}
    public List<IKeyFrame> KeyFrames{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        SlowMult = element.GetNullableFloatAttribute("SlowMult");
        NumFrames = element.GetNullableIntAttribute("NumFrames");
        StartFrame = element.GetIntAttribute("StartFrame", 0);
        HasCenter = element.HasAttribute("CenterX") || element.HasAttribute("CenterY");
        CenterX = element.GetFloatAttribute("CenterX", 0);
        CenterY = element.GetFloatAttribute("CenterY", 0);
        EaseIn = element.GetBoolAttribute("EaseIn", false);
        EaseOut = element.GetBoolAttribute("EaseOut", false);
        EasePower = element.GetIntAttribute("EasePower", 2);
        KeyFrames = element.DeserializeKeyFrameChildren();
    }
}