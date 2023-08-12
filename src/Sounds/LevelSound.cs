using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable
{
    public string SoundEventName{get; set;} = "";
    public int Delay{get; set;}
    public int Interval{get; set;}
    
    public virtual void Deserialize(XElement element)
    {
        SoundEventName = element.GetAttribute("SoundEventName");
        Delay = element.GetIntAttribute("Delay", 0);
        Interval = element.GetIntAttribute("Interval", 0);
    }
}