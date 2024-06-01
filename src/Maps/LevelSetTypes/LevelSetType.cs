using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSetType : IDeserializable, ISerializable
{
    private const string LEVEL_SET_TYPE_TEMPLATE_NAME = "Auto";
    private const string SKIP_ORDER_VALIDATION_TEMPLATE_STRING = "Don't abuse this to be lazy. It's for special cases like Bubble Tag where we explicitly want the order to be different.";

    public string LevelSetName { get; set; } = null!;

    public string DisplayNameKey { get; set; } = null!;
    public uint LevelSetID { get; set; }
    public string[] LevelTypes { get; set; } = null!;
    public bool? SkipOrderValidation { get; set; }

    public void Deserialize(XElement e)
    {
        LevelSetName = e.GetAttribute("LevelSetName");
        DisplayNameKey = e.GetElementValue("DisplayNameKey")!;
        LevelSetID = Utils.ParseUIntOrNull(e.GetElementValue("LevelSetID")) ?? 0;
        LevelTypes = e.GetElementValue("LevelTypes")?.Split(",") ?? [];
        SkipOrderValidation = Utils.ParseBoolOrNull(e.GetElementValue("SkipOrderValidation"));
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelSetName", LevelSetName);

        e.AddChild("DisplayNameKey", DisplayNameKey);
        e.AddChild("LevelSetID", LevelSetID);

        if (LevelSetName == LEVEL_SET_TYPE_TEMPLATE_NAME)
        {
            e.AddChild("SkipOrderValidation", SKIP_ORDER_VALIDATION_TEMPLATE_STRING);
            return;
        }

        e.AddIfNotNull("SkipOrderValidation", SkipOrderValidation);
        if (LevelTypes.Length > 0)
            e.AddChild("LevelTypes", string.Join(",", LevelTypes));
    }
}