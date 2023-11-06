using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class KeyFrame : AbstractKeyFrame
{
    public double X{get; set;}
    public double Y{get; set;}
    public int FrameNum{get; set;}
    public double Rotation{get; set;}
    public double? CenterX{get; set;}
    public double? CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int EasePower{get; set;}

    public bool HasCenter => CenterX is not null || CenterY is not null;

    public override void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        FrameNum = element.GetIntAttribute("FrameNum", 0);
        //Unlike other rotations, this one doesn't get translated to radians.
        Rotation = element.GetFloatAttribute("Rotation", 0);
        if(element.HasAttribute("CenterX") || element.HasAttribute("CenterY"))
        {
            CenterX = element.GetFloatAttribute("CenterX", 0);
            CenterY = element.GetFloatAttribute("CenterY", 0);
        }
        EaseIn = element.GetBoolAttribute("EaseIn", false);
        EaseOut = element.GetBoolAttribute("EaseOut", false);
        EasePower = element.GetIntAttribute("EasePower", 2);
    }

    public override XElement Serialize()
    {
        XElement e = new("KeyFrame");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("FrameNum", FrameNum.ToString());
        if(Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());
        if(HasCenter)
        {
            e.SetAttributeValue("CenterX", (CenterX??0).ToString());
            e.SetAttributeValue("CenterY", (CenterY??0).ToString());
        }
        if(EaseIn)
            e.SetAttributeValue("EaseIn", EaseIn.ToString().ToLower());
        if(EaseOut)
            e.SetAttributeValue("EaseOut", EaseOut.ToString().ToLower());
        if(EasePower != 2)
            e.SetAttributeValue("EasePower", EasePower.ToString());

        return e;
    }
}