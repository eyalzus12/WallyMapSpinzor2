using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable, ISerializable
{
    public string SoundEventName{get; set;} = null!;
    public int Delay{get; set;}
    public int Interval{get; set;}
    
    public void Deserialize(XElement element)
    {
        SoundEventName = element.GetAttribute("SoundEventName");
        Delay = element.GetIntAttribute("Delay", 0);
        Interval = element.GetIntAttribute("Interval", 0);
    }

    public XElement Serialize()
    {
        XElement e = new("LevelSound");

        e.SetAttributeValue("SoundEventName", SoundEventName);
        e.SetAttributeValue("Delay", Delay.ToString());
        e.SetAttributeValue("Interval", Interval.ToString());

        return e;
    }
}