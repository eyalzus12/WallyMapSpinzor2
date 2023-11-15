using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class KeyFrame : AbstractKeyFrame
{
    public int FrameNum{get; set;}
    public double Rotation{get; set;}
    public double? CenterX{get; set;}
    public double? CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int EasePower{get; set;}
    public double X{get; set;}
    public double Y{get; set;}

    public bool HasCenter => CenterX is not null || CenterY is not null;

    public override void Deserialize(XElement e)
    {
        FrameNum = e.GetIntAttribute("FrameNum", 0);
        //Unlike other rotations, this one doesn't get translated to radians.
        Rotation = e.GetFloatAttribute("Rotation", 0);
        if(e.HasAttribute("CenterX") || e.HasAttribute("CenterY"))
        {
            CenterX = e.GetFloatAttribute("CenterX", 0);
            CenterY = e.GetFloatAttribute("CenterY", 0);
        }
        EaseIn = e.GetBoolAttribute("EaseIn", false);
        EaseOut = e.GetBoolAttribute("EaseOut", false);
        EasePower = e.GetIntAttribute("EasePower", 2);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("FrameNum", FrameNum.ToString());
        if(Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());
        if(HasCenter)
        {
            if(CenterX != 0 || CenterY == 0)
                e.SetAttributeValue("CenterX", (CenterX??0).ToString());
            if(CenterY != 0 || CenterX == 0)
                e.SetAttributeValue("CenterY", (CenterY??0).ToString());
        }
        if(EaseIn)
            e.SetAttributeValue("EaseIn", EaseIn.ToString().ToLower());
        if(EaseOut)
            e.SetAttributeValue("EaseOut", EaseOut.ToString().ToLower());
        if(EasePower != 2)
            e.SetAttributeValue("EasePower", EasePower.ToString());
        if(X != 0)
            e.SetAttributeValue("X", X.ToString());
        if(Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
    }

    public override double GetStartFrame() => FrameNum;
    public override (double, double) GetPosition() => (X, Y);

    public override (double, double) LerpTo<T>
    (T kk, Animation.AnimationDefaultValues defaults, double time, double kTimeOffset = 0)
    {
        if(kk is KeyFrame k)
        {
            double w = (k.FrameNum + kTimeOffset - FrameNum)/(time - FrameNum);
            w = BrawlhallaMath.EaseWeight(w,
                EaseIn || defaults.EaseIn,
                EaseOut || defaults.EaseOut,
                EasePower == 2 ? defaults.EasePower : EasePower
            );
            if(CenterX is not null || CenterY is not null || defaults.CenterX is not null || defaults.CenterY is not null)
                return BrawlhallaMath.LerpWithCenter(X, Y, k.X, k.Y, CenterX??defaults.CenterX??0, CenterY??defaults.CenterY??0, w);
            else
                return BrawlhallaMath.Lerp(X, Y, k.X, k.Y, w);
        }
        else if(kk is Phase p)
        {
            return LerpTo(p.KeyFrames[0], defaults, time, kTimeOffset + p.StartFrame);
        }
        else
            throw new ArgumentException($"Keyframe cannot interpolate to unknown abstract keyframe type {typeof(T).Name}");
    }
}