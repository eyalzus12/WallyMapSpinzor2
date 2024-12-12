using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonCombo : IDeserializable, ISerializable
{
    public ComboPart[] ComboParts { get; set; } = [];
    public uint Forgiveness { get; set; }
    public bool ShowStun { get; set; }
    public bool ResetBot { get; set; }
    public uint ReplayEnd { get; set; }
    public ComboInputFlags[] DemoRecording { get; set; } = [];
    public ComboInputFlags[] BotRecording { get; set; } = [];

    public void Deserialize(XElement e)
    {
        ComboParts = e.Elements().Select((child) =>
        {
            ComboPart? part = ComboPart.New(child.Name.LocalName);
            part?.Deserialize(child);
            return part;
        }).Where((part) => part is not null).ToArray()!;

        Forgiveness = e.GetUIntElement("forgiveness", 32);
        ShowStun = e.GetBoolElement("showStun", false);
        ResetBot = e.GetBoolElement("resetBot", false);
        ReplayEnd = e.GetUIntElement("replayEnd", 1000);
        DemoRecording = e.GetElementOrNull("demoRecording")?.Split(',').Select(ushort.Parse).Cast<ComboInputFlags>().ToArray() ?? [];
        BotRecording = e.GetElementOrNull("botRecording")?.Split(',').Select(ushort.Parse).Cast<ComboInputFlags>().ToArray() ?? [];
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(ComboParts);

        e.AddChild("forgiveness", Forgiveness);
        e.AddChild("showStun", ShowStun);
        e.AddChild("resetBot", ResetBot);
        if (ReplayEnd != 1000)
            e.AddChild("replayEnd", ReplayEnd);
        if (DemoRecording.Length > 0)
            e.AddChild("demoRecording", string.Join(',', DemoRecording.Cast<ushort>()));
        if (BotRecording.Length > 0)
            e.AddChild("botRecording", string.Join(',', BotRecording.Cast<ushort>()));
    }
}