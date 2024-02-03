using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelType : IDeserializable, ISerializable
{
    private const string TEMPLATE_LEVEL_TYPE = "Template";

    public string LevelName { get; set; } = null!;
    public bool DevOnly { get; set; }
    public bool TestLevel { get; set; }

    public string DisplayName { get; set; } = null!;
    public int LevelID { get; set; } //max 255
    public string[]? TeamColorOrder { get; set; }
    public string? FileName { get; set; }
    public string? AssetName { get; set; }
    public CrateColor? CrateColorA { get; set; }
    public CrateColor? CrateColorB { get; set; }

    //only null for random LevelType
    public int? LeftKill { get; set; }
    public int? RightKill { get; set; }
    public int? TopKill { get; set; }
    public int? BottomKill { get; set; }

    public bool? SoftTopKill { get; set; }
    public bool? HardLeftKill { get; set; } //LeftKill needs to be equal or more than 200 for this to be true
    public bool? HardRightKill { get; set; } //RightKill needs to be equal or more than 200 for this to be true
    public string? BGMusic { get; set; } = null!;
    public string? ThumbnailPNGFile { get; set; }
    public bool? AIStrictRecover { get; set; }

    //used nowhere except in the template
    public uint? MidgroundTint { get; set; }
    public uint? MidgroundOffset { get; set; }
    public double? MidgroundFraction { get; set; }

    public uint? BotTint { get; set; } //always 0 in xml
    public uint? BotOffset { get; set; }
    public double? BotFraction { get; set; }

    public bool? ShowPlatsDuringMove { get; set; }
    public bool? NegateOverlaps { get; set; }
    public int? StartFrame { get; set; }
    public bool? FixedCamera { get; set; }
    public bool? AllowItemSpawnOverlap { get; set; }
    public List<string> ColorExclusionList { get; set; } = null!;
    public bool? FixedWidth { get; set; }
    public double? AIPanicLine { get; set; }
    public double? AIGroundLine { get; set; }

    //not used in LevelTypes.xml, but they exist in the code
    public int? ShadowTint { get; set; }
    public string? ItemOverride { get; set; }

    public void Deserialize(XElement e)
    {
        LevelName = e.GetAttribute("LevelName");
        DevOnly = e.GetBoolAttribute("DevOnly");
        TestLevel = e.GetBoolAttribute("TestLevel");

        DisplayName = e.GetElementValue("DisplayName")!;
        LevelID = Utils.ParseIntOrNull(e.GetElementValue("LevelID")) ?? 0;
        TeamColorOrder = e.GetElementValue("TeamColorOrder")?.Split(',');
        FileName = e.GetElementValue("FileName");
        AssetName = e.GetElementValue("AssetName");

        uint? colA = Utils.ParseUIntOrNull(e.GetElementValue("CrateColorA"));
        CrateColorA = colA is null ? null : CrateColor.FromHex((uint)colA);
        uint? colB = Utils.ParseUIntOrNull(e.GetElementValue("CrateColorB"));
        CrateColorB = colB is null ? null : CrateColor.FromHex((uint)colB);

        LeftKill = Utils.ParseIntOrNull(e.GetElementValue("LeftKill"));
        RightKill = Utils.ParseIntOrNull(e.GetElementValue("RightKill"));
        TopKill = Utils.ParseIntOrNull(e.GetElementValue("TopKill"));
        BottomKill = Utils.ParseIntOrNull(e.GetElementValue("BottomKill"));
        SoftTopKill = Utils.ParseBoolOrNull(e.GetElementValue("SoftTopKill"));
        HardLeftKill = Utils.ParseBoolOrNull(e.GetElementValue("HardLeftKill"));
        HardRightKill = Utils.ParseBoolOrNull(e.GetElementValue("HardRightKill"));
        BGMusic = e.GetElementValue("BGMusic");
        ThumbnailPNGFile = e.GetElementValue("ThumbnailPNGFile");
        AIStrictRecover = Utils.ParseBoolOrNull(e.GetElementValue("AIStrictRecover"));
        MidgroundTint = Utils.ParseUIntOrNull(e.GetElementValue("MidgroundTint"));
        MidgroundOffset = Utils.ParseUIntOrNull(e.GetElementValue("MidgroundOffset"));
        MidgroundFraction = Utils.ParseFloatOrNull(e.GetElementValue("MidgroundFraction"));
        BotTint = Utils.ParseUIntOrNull(e.GetElementValue("BotTint"));
        BotOffset = Utils.ParseUIntOrNull(e.GetElementValue("BotOffset"));
        BotFraction = Utils.ParseFloatOrNull(e.GetElementValue("BotFraction"));
        ShowPlatsDuringMove = Utils.ParseBoolOrNull(e.GetElementValue("ShowPlatsDuringMove"));
        NegateOverlaps = Utils.ParseBoolOrNull(e.GetElementValue("NegateOverlaps"));
        StartFrame = Utils.ParseIntOrNull(e.GetElementValue("StartFrame"));
        FixedCamera = Utils.ParseBoolOrNull(e.GetElementValue("FixedCamera"));
        AllowItemSpawnOverlap = Utils.ParseBoolOrNull(e.GetElementValue("AllowItemSpawnOverlap"));
        // There is one case where there are two ColorExclusionList's. The last one overrides, so we do this.
        ColorExclusionList = e.Elements("ColorExclusionList").LastOrDefault()?.Value.Split(',').ToList() ?? new();
        FixedWidth = Utils.ParseBoolOrNull(e.GetElementValue("FixedWidth"));
        AIPanicLine = Utils.ParseFloatOrNull(e.GetElementValue("AIPanicLine"));
        AIGroundLine = Utils.ParseFloatOrNull(e.GetElementValue("AIGroundLine"));
        ShadowTint = Utils.ParseIntOrNull(e.GetElementValue("ShadowTint"));

        string itemOverrideString = string.Join(',', e.Elements("ItemOverride").Select(ee => e.Value));
        ItemOverride = itemOverrideString == "" ? null : itemOverrideString;
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelName", LevelName);
        e.SetAttributeValue("DevOnly", DevOnly);
        e.SetAttributeValue("TestLevel", TestLevel);

        e.Add(new XElement("DisplayName", DisplayName));
        e.Add(new XElement("LevelID", LevelID));
        if (TeamColorOrder is not null)
            e.Add(new XElement("TeamColorOrder", string.Join(',', TeamColorOrder)));
        e.AddIfNotNull("FileName", FileName);
        e.AddIfNotNull("AssetName", AssetName);
        e.AddIfNotNull("CrateColorA", CrateColorA?.ToHexString());
        e.AddIfNotNull("CrateColorB", CrateColorB?.ToHexString());
        e.AddIfNotNull("LeftKill", LeftKill);
        e.AddIfNotNull("RightKill", RightKill);
        e.AddIfNotNull("TopKill", TopKill);
        e.AddIfNotNull("BottomKill", BottomKill);
        e.AddIfNotNull("SoftTopKill", SoftTopKill);

        if (HardLeftKill is not null)
            e.Add(new XElement("HardLeftKill", (bool)HardLeftKill && LeftKill >= 200));
        if (HardRightKill is not null)
            e.Add(new XElement("HardRightKill", (bool)HardRightKill && RightKill >= 200));

        e.AddIfNotNull("BGMusic", BGMusic);
        e.AddIfNotNull("FixedWidth", FixedWidth?.ToString()?.ToUpperInvariant());
        e.AddIfNotNull("ThumbnailPNGFile", ThumbnailPNGFile);
        e.AddIfNotNull("AIStrictRecover", AIStrictRecover?.ToString()?.ToUpperInvariant());

        if (MidgroundTint is not null)
            e.Add(new XElement("MidgroundTint", "0x" + MidgroundTint?.ToString("X6")));
        if (MidgroundOffset is not null)
            e.Add(new XElement("MidgroundOffset", "0x" + MidgroundTint?.ToString("X6")));
        e.AddIfNotNull("MidgroundFraction", MidgroundFraction);

        if (BotTint == 0) e.Add(new XElement("BotTint", 0));
        else if (BotTint > 0) e.Add(new XElement("BotTint", "0x" + BotTint?.ToString("X6")));
        if (LevelName == TEMPLATE_LEVEL_TYPE) e.Add(new XElement("BotOffset", 0));
        else if (BotOffset is not null) e.Add(new XElement("BotOffset", "0x" + BotOffset?.ToString("X6")));
        e.AddIfNotNull("BotFraction", BotFraction);

        e.AddIfNotNull("ShowPlatsDuringMove", ShowPlatsDuringMove);
        e.AddIfNotNull("NegateOverlaps", NegateOverlaps);
        e.AddIfNotNull("StartFrame", StartFrame);
        e.AddIfNotNull("FixedCamera", FixedCamera);
        e.AddIfNotNull("AllowItemSpawnOverlap", AllowItemSpawnOverlap);

        if (ColorExclusionList.Count > 0)
            e.Add(new XElement("ColorExclusionList", string.Join(',', ColorExclusionList)));

        e.AddIfNotNull("AIGroundLine", AIGroundLine);
        e.AddIfNotNull("AIPanicLine", AIPanicLine);

        if (ShadowTint is not null)
            e.Add(new XElement("ShadowTint", ShadowTint));

        // it is unclear how ItemOverride would be formatted, but this is my best guess
        // due to how it works
        if (ItemOverride is not null)
        {
            string[] split = ItemOverride.Split(',');
            for (int i = 0; i < split.Length - 1; i += 2)
            {
                e.Add(new XElement("ItemOverride", $"{split[i]},{split[i + 1]}"));
            }
            if (split.Length % 2 != 0)
                e.Add(new XElement("ItemOverride", split[^1]));
        }
    }
}
