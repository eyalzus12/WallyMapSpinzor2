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