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
    public List<AbstractKeyFrame> KeyFrames { get; set; } = null!;

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
            e.SetAttributeValue("EaseIn", EaseIn.ToString().ToLower());
        if (EaseOut)
            e.SetAttributeValue("EaseOut", EaseOut.ToString().ToLower());
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

    public (double, double) GetOffset(RenderData data, TimeSpan time)
    {
        double numframes = NumFrames ?? data.DefaultNumFrames ?? 0;
        double slowmult = SlowMult ?? data.DefaultSlowMult ?? 1;
        double frame = FRAME_MULTIPLIER * (60.0 * time.TotalSeconds);
        frame /= slowmult; //slow mult
        frame += StartFrame; //apply start frame
        frame += 1; //frames start at 1
        double frameInRange = BrawlhallaMath.SafeMod(frame, numframes);
        //find the keyframe pair
        int i = 0;
        for (; i < KeyFrames.Count; ++i)
        {
            if (KeyFrames[i].GetStartFrame() >= frameInRange) break;
        }

        if (i == KeyFrames.Count) i = 0;
        int j = (i == 0 ? KeyFrames.Count : i) - 1;
        //lerp
        return KeyFrames[j].LerpTo(KeyFrames[i],
            new(CenterX, CenterY, EaseIn, EaseOut, EasePower),
            numframes,
            frame,
            0,
            0
        );
    }
}
