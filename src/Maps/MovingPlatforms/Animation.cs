using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Animation : IDeserializable, ISerializable
{
    //these two can get a default from LevelDesc
    public double? SlowMult{get; set;}
    public int? NumFrames{get; set;}
    public int StartFrame{get; set;}
    public double? CenterX{get; set;}
    public double? CenterY{get; set;}
    public bool EaseIn{get; set;}
    public bool EaseOut{get; set;}
    public int EasePower{get; set;}
    public List<AbstractKeyFrame> KeyFrames{get; set;} = null!;
    
    public bool HasCenter => CenterX is not null || CenterY is not null;
    public void Deserialize(XElement element)
    {
        SlowMult = element.GetNullableFloatAttribute("SlowMult");
        NumFrames = element.GetNullableIntAttribute("NumFrames");
        StartFrame = element.GetIntAttribute("StartFrame", 0);
        if(element.HasAttribute("CenterX") || element.HasAttribute("CenterY"))
        {
            CenterX = element.GetFloatAttribute("CenterX", 0);
            CenterY = element.GetFloatAttribute("CenterY", 0);
        }
        EaseIn = element.GetBoolAttribute("EaseIn", false);
        EaseOut = element.GetBoolAttribute("EaseOut", false);
        EasePower = element.GetIntAttribute("EasePower", 2);
        KeyFrames = element.DeserializeKeyFrameChildren();
    }

    public XElement Serialize()
    {
        XElement e = new("Animation");
        
        if(SlowMult is not null)
            e.SetAttributeValue("SlowMult", SlowMult.ToString());
        if(NumFrames is not null)
            e.SetAttributeValue("NumFrames", NumFrames.ToString());
        e.SetAttributeValue("StartFrame", StartFrame.ToString());
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
        foreach(AbstractKeyFrame k in KeyFrames)
            e.Add(k.Serialize());

        return e;
    }
}