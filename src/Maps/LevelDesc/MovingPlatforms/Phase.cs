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

    public override double GetStartFrame() => StartFrame + KeyFrames[0].GetStartFrame();
    public override (double, double) GetPosition() => KeyFrames[0].GetPosition();

    public override (double, double) LerpTo<T>
    (T kk, Animation.AnimationDefaultValues defaults, double numframes, double frame, double fromTimeOffset, double toTimeOffset)
    {
        double _frame = BrawlhallaMath.SafeMod(frame, numframes);
        //this happens when the animation resets and the time becomes close to 0
        //so as a hacky fix we readjust the time to match the "future"
        if(_frame < StartFrame)
            _frame += numframes;
        
        //find the keyframe pair
        int i = 0;
        for(; i < KeyFrames.Count; ++i)
        {
            if(StartFrame + KeyFrames[i].GetStartFrame() >= _frame) break;
        }

        //need to interpolate from phase end
        if(i == KeyFrames.Count)
        {
            return KeyFrames.Last().LerpTo(kk, defaults, numframes, frame, fromTimeOffset + StartFrame, toTimeOffset);
        }
        //interpolation is inside phase
        else
        {
            return KeyFrames[i-1].LerpTo(KeyFrames[i], defaults, numframes, frame, fromTimeOffset + StartFrame, toTimeOffset + StartFrame);
        }
    }
}