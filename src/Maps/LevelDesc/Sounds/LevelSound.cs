using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable, ISerializable
{
    public string SoundEventName { get; set; } = null!;
    public uint Interval { get; set; }
    public uint Delay { get; set; }
    public int OnlineDelayDiff { get; set; }
    public int TotalLoops { get; set; } // 0 means infinite
    public bool IgnoreOnBlurBG { get; set; }

    public void Deserialize(XElement e)
    {
        SoundEventName = e.GetAttribute("SoundEventName");
        Interval = e.GetUIntAttribute("Interval", 0);
        Delay = e.GetUIntAttribute("Delay", 0);
        OnlineDelayDiff = e.GetIntAttribute("OnlineDelayDiff", 0);
        TotalLoops = e.GetIntAttribute("TotalLoops", 0);
        IgnoreOnBlurBG = e.GetBoolAttribute("IgnoreOnBlurBG", false);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("SoundEventName", SoundEventName);
        e.SetAttributeValue("Interval", Interval);
        e.SetAttributeValue("Delay", Delay);
        if (OnlineDelayDiff != 0)
            e.SetAttributeValue("OnlineDelayDiff", OnlineDelayDiff);
        if (TotalLoops != 0)
            e.SetAttributeValue("TotalLoops", TotalLoops);
        if (IgnoreOnBlurBG)
            e.SetAttributeValue("IgnoreOnBlurBG", IgnoreOnBlurBG);
    }
}