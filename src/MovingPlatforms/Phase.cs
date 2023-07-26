using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Phase : IDeserializable, IKeyFrame
{
    public int StartFrame{get; set;}

    public List<IKeyFrame> KeyFrames{get; set;} = new();

    public void Deserialize(XElement element)
    {
        StartFrame = element.GetIntAttribute("StartFrame", 0);
        KeyFrames = element.DeserializeKeyFrameChildren();
    }
}