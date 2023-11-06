using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Phase : AbstractKeyFrame
{
    public int StartFrame{get; set;}

    public List<AbstractKeyFrame> KeyFrames{get; set;} = null!;

    public override void Deserialize(XElement element)
    {
        StartFrame = element.GetIntAttribute("StartFrame", 0);
        KeyFrames = element.DeserializeKeyFrameChildren();
    }

    public override XElement Serialize()
    {
        XElement e = new("Phase");

        e.SetAttributeValue("StartFrame", StartFrame);
        foreach(AbstractKeyFrame k in KeyFrames)
            e.Add(k.Serialize());

        return e;
    }
}