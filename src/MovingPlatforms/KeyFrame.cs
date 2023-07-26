using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class KeyFrame : IDeserializable, IKeyFrame
{
    public double X{get; set;}
    public double Y{get; set;}
    public int FrameNum{get; set;}
    public double Rotation{get; set;}
    public bool HasCenter{get; set;}
    public double CenterX{get; set;}
    public double CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int? EasePower{get; set;}

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        FrameNum = element.GetIntAttribute("FrameNum", 0);
        //Unlike other rotations, this one doesn't get translated to radians.
        Rotation = element.GetFloatAttribute("Rotation", 0);
        HasCenter = element.HasAttribute("CenterX") || element.HasAttribute("CenterY");
        CenterX = element.GetFloatAttribute("CenterX", 0);
        CenterY = element.GetFloatAttribute("CenterY", 0);
        EaseIn = element.GetBoolAttribute("EaseIn", false);
        EaseOut = element.GetBoolAttribute("EaseOut", false);
        EasePower = element.GetNullableIntAttribute("EasePower");
    }
}