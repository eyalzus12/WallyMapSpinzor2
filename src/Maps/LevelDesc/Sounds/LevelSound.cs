using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable, ISerializable
{
    public string SoundEventName{get; set;} = null!;
    public int Interval{get; set;}
    public int Delay{get; set;}
    
    public void Deserialize(XElement e)
    {
        SoundEventName = e.GetAttribute("SoundEventName");
        Interval = e.GetIntAttribute("Interval", 0);
        Delay = e.GetIntAttribute("Delay", 0);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("SoundEventName", SoundEventName);
        e.SetAttributeValue("Interval", Interval.ToString());
        e.SetAttributeValue("Delay", Delay.ToString());
    }
}
