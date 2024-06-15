using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Animation : IDeserializable, ISerializable
{
    //these two can get a default from LevelDesc
    public int? NumFrames { get; set; }
    public double? SlowMult { get; set; }
    public double? CenterX { get; set; }
    public double? CenterY { get; set; }
    public bool EaseIn { get; set; }
    public bool EaseOut { get; set; }
    public int EasePower { get; set; }
    public int StartFrame { get; set; }
    public AbstractKeyFrame[] KeyFrames { get; set; } = null!;

    public bool HasCenter => CenterX is not null || CenterY is not null;
    public void Deserialize(XElement e)
    {
        NumFrames = e.GetIntAttributeOrNull("NumFrames");
        SlowMult = e.GetFloatAttributeOrNull("SlowMult");
        if (e.HasAttribute("CenterX") || e.HasAttribute("CenterY"))
        {
            CenterX = e.GetFloatAttribute("CenterX", 0);
            CenterY = e.GetFloatAttribute("CenterY", 0);
        }
        EaseIn = e.GetBoolAttribute("EaseIn", false);
        EaseOut = e.GetBoolAttribute("EaseOut", false);
        EasePower = e.GetIntAttribute("EasePower", 2);
        StartFrame = e.GetIntAttribute("StartFrame", 0);
        KeyFrames = e.DeserializeKeyFrameChildren();
    }

    public void Serialize(XElement e)
    {
        if (NumFrames is not null)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        if (SlowMult is not null)
            e.SetAttributeValue("SlowMult", SlowMult.ToString());
        if (HasCenter)
        {
            if (CenterX != 0 || CenterY == 0)
                e.SetAttributeValue("CenterX", (CenterX ?? 0).ToString());
            if (CenterY != 0 || CenterX == 0)
                e.SetAttributeValue("CenterY", (CenterY ?? 0).ToString());
        }
        if (EaseIn)
            e.SetAttributeValue("EaseIn", EaseIn.ToString().ToLowerInvariant());
        if (EaseOut)
            e.SetAttributeValue("EaseOut", EaseOut.ToString().ToLowerInvariant());
        if (EasePower != 2)
            e.SetAttributeValue("EasePower", EasePower.ToString());
        if (StartFrame != 0)
            e.SetAttributeValue("StartFrame", StartFrame.ToString());

        e.AddManySerialized(KeyFrames);
    }

    public readonly record struct ValueDefaults(double? CenterX, double? CenterY, bool EaseIn, bool EaseOut, int EasePower)
    {

    }

    //the game wants to do 0.05, but there's a 0.96 multiplier on top of that
    //because brawlhalla stores time as 16*frames, which is 1/960th of a second (almost, but not exactly, a millisecond)
    //but instead of dividing by 16 to get the frame count, they multiply by 60/1000.
    public const double FRAME_MULTIPLIER = 0.048;

    public (double, double) GetOffset(RenderContext context, TimeSpan time)
    {
        /*double numframes = NumFrames ?? context.DefaultNumFrames ?? 0;
        double slowmult = SlowMult ?? context.DefaultSlowMult ?? 1;
        double desiredFrame = FRAME_MULTIPLIER * (60.0 * time.TotalSeconds);
        desiredFrame /= slowmult; //slow mult
        desiredFrame += StartFrame; //apply start frame
        desiredFrame = BrawlhallaMath.SafeMod(desiredFrame, numframes);
        desiredFrame += 1; //keyframes start at 1*/

        List<(double, double)> positions = [];
        int currentFrame = 1;
        List<KeyFrame> keyframes = GetImplicitKeyFrames();
        for (int i = 0; i < keyframes.Count; ++i)
        {
            KeyFrame k = keyframes[i];
            KeyFrame k2; int frame2;
            if (i == keyframes.Count - 1)
            {
                k2 = keyframes[0];
                frame2 = (NumFrames ?? context.DefaultNumFrames ?? 0) + 1;
            }
            else
            {
                k2 = keyframes[i + 1];
                frame2 = k2.FrameNum;
            }

            while (currentFrame < frame2)
            {
                double w = (currentFrame - k.FrameNum) / (double)(frame2 - k.FrameNum);
                w = BrawlhallaMath.EaseWeight(w,
                   k.EaseIn ?? EaseIn,
                   k.EaseOut ?? EaseOut,
                   k.EasePower ?? EasePower
                );
                if (k.CenterX is not null || k.CenterY is not null || CenterX is not null || CenterY is not null)
                    positions.Add(BrawlhallaMath.BrawlhallaLerpWithCenter(k.X, k.Y, k2.X, k2.Y, k.CenterX ?? CenterX ?? 0, k.CenterY ?? CenterY ?? 0, w));
                else
                    positions.Add(BrawlhallaMath.BrawlhallaLerp(k.X, k.Y, k2.X, k2.Y, w));
                currentFrame++;
            }
        }

        double brawlhallaTime = time.TotalSeconds * 60.0 * 16.0;
        double frames = 1000 * (positions.Count / 60.0) * (SlowMult ?? context.DefaultSlowMult ?? 1);
        double clampedTime = brawlhallaTime * 0.05 % frames;
        double positionIndex = StartFrame + clampedTime / frames * positions.Count;
        int wholeIndex = (int)Math.Floor(positionIndex + 1e-7);
        int nextIndex = (wholeIndex + 1) % positions.Count;
        int index = wholeIndex % positions.Count;
        double smallDiff = positionIndex - wholeIndex;
        (double p1x, double p1y) = positions[nextIndex];
        (double p2x, double p2y) = positions[index];
        return (p1x * smallDiff + p2x * (1 - smallDiff), p1y * smallDiff + p2y * (1 - smallDiff));
    }

    public List<KeyFrame> GetImplicitKeyFrames()
    {
        List<KeyFrame> result = [];
        foreach (AbstractKeyFrame akf in KeyFrames)
            akf.GetImplicitKeyFrames(result, 0, 0);
        return result;
    }
}