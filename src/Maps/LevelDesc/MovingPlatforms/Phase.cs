using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Phase : AbstractKeyFrame
{
    public int StartFrame { get; set; }

    public AbstractKeyFrame[] KeyFrames { get; set; } = null!;

    public override void Deserialize(XElement e)
    {
        StartFrame = e.GetIntAttribute("StartFrame", 0);
        KeyFrames = e.DeserializeKeyFrameChildren();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("StartFrame", StartFrame);
        e.AddManySerialized(KeyFrames);
    }

    public override void GetImplicitKeyFrames(List<KeyFrame> output, int index, int startFrame)
    {
        for (int i = 0; i < KeyFrames.Length; ++i)
            KeyFrames[i].GetImplicitKeyFrames(output, i, StartFrame);
    }
}