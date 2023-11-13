using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Phase : AbstractKeyFrame
{
    public int StartFrame{get; set;}

    public List<AbstractKeyFrame> KeyFrames{get; set;} = null!;

    public override void Deserialize(XElement e)
    {
        StartFrame = e.GetIntAttribute("StartFrame", 0);
        KeyFrames = e.DeserializeKeyFrameChildren();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("StartFrame", StartFrame);
        foreach(AbstractKeyFrame k in KeyFrames)
            e.Add(k.SerializeToXElement());
    }

    public override double GetStartFrame() => StartFrame;
    public override (double, double) GetPosition() => KeyFrames[0].GetPosition();

    public override (double, double) LerpTo<T>
    (T kk, Animation.AnimationDefaultValues defaults, double time, double kTimeOffset = 0)
    {
        return KeyFrames.Last().LerpTo(kk, defaults, time, kTimeOffset);
    }
}