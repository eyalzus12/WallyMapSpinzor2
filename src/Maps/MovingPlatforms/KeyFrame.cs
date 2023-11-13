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

    public override double GetStartFrame() => FrameNum;
    public override (double, double) GetPosition() => (X, Y);

    public override (double, double) LerpTo<T>
    (T kk, double? centerX, double? centerY, double time, double kTimeOffset = 0)
    {
        if(kk is KeyFrame k)
        {
            double w = (k.FrameNum + kTimeOffset - FrameNum)/(time - FrameNum);
            w = BrawlhallaMath.EaseWeight(w, EaseIn, EaseOut, EasePower);
            if(CenterX is not null || CenterY is not null || centerX is not null || centerY is not null)
                return BrawlhallaMath.LerpWithCenter(X, Y, k.X, k.Y, CenterX??centerX??0, CenterY??centerY??0, w);
            else
                return BrawlhallaMath.Lerp(X, Y, k.X, k.Y, w);
        }
        else if(kk is Phase p)
        {
            return LerpTo(p.KeyFrames[0], centerX, centerY, time, kTimeOffset + p.StartFrame);
        }
        else
            throw new ArgumentException($"Keyframe cannot interpolate to unknown abstract keyframe type {typeof(T).Name}");
    }
}