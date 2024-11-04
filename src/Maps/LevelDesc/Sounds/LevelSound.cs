using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSound : IDeserializable, ISerializable
{
    public string SoundEventName { get; set; } = null!;
    public uint Interval { get; set; }
    public uint Delay { get; set; }

    public void Deserialize(XElement e)
    {
        SoundEventName = e.GetAttribute("SoundEventName");
        Interval = e.GetUIntAttribute("Interval", 0);
        Delay = e.GetUIntAttribute("Delay", 0);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("SoundEventName", SoundEventName);
        e.SetAttributeValue("Interval", Interval);
        e.SetAttributeValue("Delay", Delay);
    }
}