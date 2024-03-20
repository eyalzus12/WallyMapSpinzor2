using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSetType : IDeserializable, ISerializable
{
    private const string LEVEL_SET_TYPE_TEMPLATE_NAME = "Auto";
    private const string SKIP_ORDER_VALIDATION_TEMPLATE_STRING = "Don't abuse this to be lazy. It's for special cases like Bubble Tag where we explicitly want the order to be different.";

    public string LevelSetName { get; set; } = null!;

    public string DisplayNameKey { get; set; } = null!;
    public uint LevelSetID { get; set; }
    public List<string> LevelTypes { get; set; } = null!;
    public bool? SkipOrderValidation { get; set; }

    public void Deserialize(XElement e)
    {
        LevelSetName = e.GetAttribute("LevelSetName");
        DisplayNameKey = e.GetElementValue("DisplayNameKey")!;
        LevelSetID = Utils.ParseUIntOrNull(e.GetElementValue("LevelSetID")) ?? 0;
        LevelTypes = e.GetElementValue("LevelTypes")?.Split(",").ToList() ?? [];
        SkipOrderValidation = Utils.ParseBoolOrNull(e.GetElementValue("SkipOrderValidation"));
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelSetName", LevelSetName);

        e.Add(new XElement("DisplayNameKey", DisplayNameKey));
        e.Add(new XElement("LevelSetID", LevelSetID));

        if (LevelSetName == LEVEL_SET_TYPE_TEMPLATE_NAME)
        {
            e.Add(new XElement("SkipOrderValidation", SKIP_ORDER_VALIDATION_TEMPLATE_STRING));
            return;
        }

        e.AddIfNotNull("SkipOrderValidation", SkipOrderValidation);
        if (LevelTypes.Count > 0)
            e.Add(new XElement("LevelTypes", string.Join(",", LevelTypes)));
    }
}