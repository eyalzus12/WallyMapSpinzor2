using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class KeyFrame : AbstractKeyFrame
{
    public int FrameNum { get; set; }
    public double Rotation { get; set; }
    public double? CenterX { get; set; }
    public double? CenterY { get; set; }
    public bool? EaseIn { get; set; }
    public bool? EaseOut { get; set; }
    public int? EasePower { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public bool HasCenter => CenterX is not null || CenterY is not null;

    public override void Deserialize(XElement e)
    {
        FrameNum = e.GetIntAttribute("FrameNum", 0);
        //Unlike other rotations, this one doesn't get translated to radians.
        Rotation = e.GetFloatAttribute("Rotation", 0);
        CenterX = e.GetFloatAttributeOrNull("CenterX");
        CenterY = e.GetFloatAttributeOrNull("CenterY");
        EaseIn = e.GetBoolAttributeOrNull("EaseIn");
        EaseOut = e.GetBoolAttributeOrNull("EaseOut");
        EasePower = e.GetIntAttributeOrNull("EasePower");
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("FrameNum", FrameNum);
        if (Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation);
        if (CenterX is not null)
            e.SetAttributeValue("CenterX", CenterX);
        if (CenterY is not null)
            e.SetAttributeValue("CenterY", CenterY);
        if (EaseIn is not null)
            e.SetAttributeValue("EaseIn", EaseIn);
        if (EaseOut is not null)
            e.SetAttributeValue("EaseOut", EaseOut);
        if (EasePower is not null)
            e.SetAttributeValue("EasePower", EasePower);
        if (X != 0)
            e.SetAttributeValue("X", X);
        if (Y != 0)
            e.SetAttributeValue("Y", Y);
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
                EaseIn ?? defaults.EaseIn,
                EaseOut ?? defaults.EaseOut,
                EasePower ?? defaults.EasePower
            );
            if (w < 0 || 1 < w)
                throw new InvalidOperationException($"RENDERER BUG. PLEASE REPORT. Invalid weight {w} during keyframe interpolation. From: {FrameNum}(+{fromTimeOffset}) To: {k.FrameNum}(+{toTimeOffset}). Keyframe time: {frame}. Frame diff: {fdiff}. Time diff: {tdiff}. EaseIn: {EaseIn ?? defaults.EaseIn}. EaseOut: {EaseOut ?? defaults.EaseOut}. EasePower: {EasePower ?? defaults.EasePower}");

            if (CenterX is not null || CenterY is not null || defaults.CenterX is not null || defaults.CenterY is not null)
                return BrawlhallaMath.BrawlhallaLerpWithCenter(X, Y, k.X, k.Y, CenterX ?? defaults.CenterX ?? 0, CenterY ?? defaults.CenterY ?? 0, w);
            else
                return BrawlhallaMath.BrawlhallaLerp(X, Y, k.X, k.Y, w);
        }
        else if (keyFrame is Phase p)
        {
            //has 0 frame num on first keyframe
            if (p.KeyFrames.Length >= 1 && p.KeyFrames[0].GetStartFrame() == 0)
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
                    //p.StartFrame to ensure that interpolation into a phase
                    //will be able to know if the phase started.
                    return LerpTo(p.KeyFrames[0], defaults, numframes, frame, p.StartFrame - FrameNum, p.StartFrame);
                }
            }
        }
        else
            throw new ArgumentException($"Keyframe cannot interpolate to unknown abstract keyframe type {keyFrame.GetType().Name}");
    }

    public override void GetImplicitKeyFrames(List<KeyFrame> output, int index, int startFrame)
    {
        if (startFrame > 0 && index == 0 && FrameNum + startFrame > startFrame && output.Count > 0)
        {
            KeyFrame last = output[^1];
            if (last.X != X || last.Y != Y || last.Rotation != Rotation)
            {
                output.Add(new KeyFrame()
                {
                    X = last.X,
                    Y = last.Y,
                    Rotation = last.Rotation,
                    FrameNum = startFrame
                });
            }
        }

        output.Add(new KeyFrame()
        {
            X = X,
            Y = Y,
            Rotation = Rotation,
            CenterX = CenterX,
            CenterY = CenterY,
            EaseIn = EaseIn,
            EaseOut = EaseOut,
            EasePower = EasePower,
            FrameNum = FrameNum + startFrame,
        });
    }
}