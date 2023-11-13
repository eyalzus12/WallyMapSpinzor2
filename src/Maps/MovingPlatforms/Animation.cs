using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Animation : IDeserializable, ISerializable
{
    //these two can get a default from LevelDesc
    public int? NumFrames{get; set;}
    public double? SlowMult{get; set;}
    public double? CenterX{get; set;}
    public double? CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int EasePower{get; set;}
    public int StartFrame{get; set;}
    public List<AbstractKeyFrame> KeyFrames{get; set;} = null!;
    
    public bool HasCenter => CenterX is not null || CenterY is not null;
    public void Deserialize(XElement element)
    {
        NumFrames = element.GetNullableIntAttribute("NumFrames");
        SlowMult = element.GetNullableFloatAttribute("SlowMult");
        if(element.HasAttribute("CenterX") || element.HasAttribute("CenterY"))
        {
            CenterX = element.GetFloatAttribute("CenterX", 0);
            CenterY = element.GetFloatAttribute("CenterY", 0);
        }
        EaseIn = element.GetBoolAttribute("EaseIn", false);
        EaseOut = element.GetBoolAttribute("EaseOut", false);
        EasePower = element.GetIntAttribute("EasePower", 2);
        StartFrame = element.GetIntAttribute("StartFrame", 0);
        KeyFrames = element.DeserializeKeyFrameChildren();
    }

    public XElement Serialize()
    {
        XElement e = new("Animation");
        
        if(NumFrames is not null)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        if(SlowMult is not null)
            e.SetAttributeValue("SlowMult", SlowMult.ToString());
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
        if(StartFrame != 0)
            e.SetAttributeValue("StartFrame", StartFrame.ToString());
        foreach(AbstractKeyFrame k in KeyFrames)
            e.Add(k.Serialize());

        return e;
    }

    public readonly record struct AnimationDefaultValues(double? CenterX, double? CenterY, bool EaseIn, bool EaseOut, int EasePower)
    {

    }

    public (double, double) GetOffset(GlobalRenderData rd, double time)
    {
        //apply time offsets
        time += StartFrame;
        time *= SlowMult ?? rd.DefaultSlowMult ?? 1;
        time = BrawlhallaMath.SafeMod(time, NumFrames ?? rd.DefaultNumFrames ?? 0);
        //find the keyframe pair
        int i = 0;
        for(; i < KeyFrames.Count; ++i)
        {
            if(KeyFrames[i].GetStartFrame() >= time) break;
        }
        int j = (i == 0 ? KeyFrames.Count : i) - 1;
        if(i == KeyFrames.Count) i = 0;
        //lerp
        return KeyFrames[j].LerpTo(KeyFrames[i], new(CenterX, CenterY, EaseIn, EaseOut, EasePower), time);
    }
}