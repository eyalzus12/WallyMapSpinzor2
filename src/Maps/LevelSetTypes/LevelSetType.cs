using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSetType : IDeserializable, ISerializable
{
    public string LevelSetName{get; set;} = null!;

    public string DisplayNameKey{get; set;} = null!;
    public uint LevelSetID{get; set;}
    public List<string> LevelTypes{get; set;} = null!;
    public bool? SkipOrderValidation{get; set;}

    public void Deserialize(XElement e)
    {
        LevelSetName = e.GetAttribute("LevelSetName");
        DisplayNameKey = e.Element("DisplayNameKey")!.Value;
        LevelSetID = Utils.ParseUIntOrNull(e.Element("LevelSetID")?.Value) ?? 0;
        LevelTypes = e.Element("LevelTypes")?.Value.Split(",").ToList() ?? new();
        SkipOrderValidation = Utils.ParseBoolOrNull(e.Element("SkipOrderValidation")?.Value);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelSetName", LevelSetName);

        e.Add(new XElement("DisplayNameKey", DisplayNameKey));
        e.Add(new XElement("LevelSetID", LevelSetID));

        if (LevelSetName == "Auto")
        {
            e.Add(new XElement("SkipOrderValidation", "Don't abuse this to be lazy. It's for special cases like Bubble Tag where we explicitly want the order to be different."));
            return;
        }

        e.AddIfNotNull("SkipOrderValidation", SkipOrderValidation);
        if (LevelTypes.Count > 0)
            e.Add(new XElement("LevelTypes", string.Join(",", LevelTypes)));
    }
}