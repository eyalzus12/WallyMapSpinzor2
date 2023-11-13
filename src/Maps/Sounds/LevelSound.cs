using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable, ISerializable
{
    public string SoundEventName{get; set;} = null!;
    public int Interval{get; set;}
    public int Delay{get; set;}
    
    public void Deserialize(XElement element)
    {
        SoundEventName = element.GetAttribute("SoundEventName");
        Interval = element.GetIntAttribute("Interval", 0);
        Delay = element.GetIntAttribute("Delay", 0);
    }

    public XElement Serialize()
    {
        XElement e = new("LevelSound");

        e.SetAttributeValue("SoundEventName", SoundEventName);
        e.SetAttributeValue("Interval", Interval.ToString());
        e.SetAttributeValue("Delay", Delay.ToString());

        return e;
    }
}