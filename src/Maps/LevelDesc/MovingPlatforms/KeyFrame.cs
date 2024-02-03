using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class KeyFrame : AbstractKeyFrame
{
    public int FrameNum { get; set; }
    public double Rotation { get; set; }
    public double? CenterX { get; set; }
    public double? CenterY { get; set; }
    public bool EaseIn { get; set; }
    public bool EaseOut { get; set; }
    public int EasePower { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public bool HasCenter => CenterX is not null || CenterY is not null;

    public override void Deserialize(XElement e)
    {
        FrameNum = e.GetIntAttribute("FrameNum", 0);
        //Unlike other rotations, this one doesn't get translated to radians.
        Rotation = e.GetFloatAttribute("Rotation", 0);
        if (e.HasAttribute("CenterX") || e.HasAttribute("CenterY"))
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
        if (Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());
        if (HasCenter)
        {
            if (CenterX != 0 || CenterY == 0)
                e.SetAttributeValue("CenterX", (CenterX ?? 0).ToString());
            if (CenterY != 0 || CenterX == 0)
                e.SetAttributeValue("CenterY", (CenterY ?? 0).ToString());
        }
        if (EaseIn)
            e.SetAttributeValue("EaseIn", EaseIn.ToString().ToLower());
        if (EaseOut)
            e.SetAttributeValue("EaseOut", EaseOut.ToString().ToLower());
        if (EasePower != 2)
            e.SetAttributeValue("EasePower", EasePower.ToString());
        if (X != 0)
            e.SetAttributeValue("X", X.ToString());
        if (Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
    }

    public override double GetStartFrame() => FrameNum;
    public override (double, double) GetPosition() => (X, Y);

    public override (double, double) LerpTo(AbstractKeyFrame keyFrame, Animation.ValueDefaults defaults, double numframes, double frame, double fromTimeOffset, double toTimeOffset)
    {
        if (keyFrame is KeyFrame k)
        {
            double fdiff = k.FrameNum + toTimeOffset - FrameNum - fromTimeOffset;
            fdiff = BrawlhallaMath.SafeMod(fdiff, numframes);
            double tdiff = frame - FrameNum - fromTimeOffset;
            tdiff = BrawlhallaMath.SafeMod(tdiff, numframes);
            double w = tdiff / fdiff;
            w = BrawlhallaMath.EaseWeight(w,
                EaseIn || defaults.EaseIn,
                EaseOut || defaults.EaseOut,
                EasePower == 2 ? defaults.EasePower : EasePower
            );
            if (w < 0 || 1 < w)
                throw new InvalidOperationException($"RENDERER BUG. PLEASE REPORT. Invalid weight {w} during keyframe interpolation. From: {FrameNum}(+{fromTimeOffset}) To: {k.FrameNum}(+{toTimeOffset}). Keyframe time: {frame}. Frame diff: {fdiff}. Time diff: {tdiff}.");

            if (CenterX is not null || CenterY is not null || defaults.CenterX is not null || defaults.CenterY is not null)
                return BrawlhallaMath.BrawlhallaLerpWithCenter(X, Y, k.X, k.Y, CenterX ?? defaults.CenterX ?? 0, CenterY ?? defaults.CenterY ?? 0, w);
            else
                return BrawlhallaMath.BrawlhallaLerp(X, Y, k.X, k.Y, w);
        }
        else if (keyFrame is Phase p)
        {
            //has 0 frame num on first keyframe
            if (p.KeyFrames[0].GetStartFrame() == 0)
            {
                return LerpTo(p.KeyFrames[0], defaults, numframes, frame, fromTimeOffset, toTimeOffset + p.StartFrame);
            }
            //non-0 frame num on first keyframe. gotta wait for phase start.
            else
            {
                //phase hasn't started. remain in position.
                if (toTimeOffset + p.StartFrame >= BrawlhallaMath.SafeMod(frame, numframes))
                {
                    return (X, Y);
                }
                //phase started
                else
                {
                    //use -FrameNum to fake a keyframe at the start of the phase.
                    //this will be as if we are interpolating from keyframe with framenum 0.
                    //we pass toTimeOffset + p.StartFrame to ensure that interpolation into a phase
                    //will be able to know if the phase started.
                    return LerpTo(p.KeyFrames[0], defaults, numframes, frame, toTimeOffset + p.StartFrame - FrameNum, toTimeOffset + p.StartFrame);
                }
            }
        }
        else
            throw new ArgumentException($"Keyframe cannot interpolate to unknown abstract keyframe type {keyFrame.GetType().Name}");
    }
}
