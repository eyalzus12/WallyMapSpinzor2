using System;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelType : IDeserializable, ISerializable
{
    public enum TeamColorEnum
    {
        Default, // this is used as a fallback
        Red,
        Blue,
        Yellow,
        Purple,
    }

    private const string TEMPLATE_LEVEL_TYPE = "Template";
    private static readonly TeamColorEnum[] DEFAULT_TEAM_COLOR_ORDER = [TeamColorEnum.Red, TeamColorEnum.Blue, TeamColorEnum.Yellow, TeamColorEnum.Purple];

    public string LevelName { get; set; } = null!;
    public bool DevOnly { get; set; }
    public bool TestLevel { get; set; }

    public string DisplayName { get; set; } = null!;
    public int LevelID { get; set; } //max 255
    public TeamColorEnum[] TeamColorOrder { get; set; } = DEFAULT_TEAM_COLOR_ORDER;
    public TeamColorEnum AvoidTeamColor { get; set; }
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
    public string? BGMusic { get; set; }
    public string? StreamerBGMusic { get; set; }
    public string? ThumbnailPNGFile { get; set; }
    public bool? AIStrictRecover { get; set; }

    //used nowhere except in the template
    public uint? MidgroundTint { get; set; }
    public uint? MidgroundOffset { get; set; } // default to 0
    public double? MidgroundFraction { get; set; }

    public uint? BotTint { get; set; }
    public uint? BotOffset { get; set; }
    public double? BotFraction { get; set; } // defaults to 0.5

    public bool? ShowPlatsDuringMove { get; set; }
    public bool? ShowLavaLevelDuringMove { get; set; }
    public bool? NegateOverlaps { get; set; }
    public int? StartFrame { get; set; }
    public bool? FixedCamera { get; set; }
    public bool? AllowItemSpawnOverlap { get; set; }
    public string[] ColorExclusionList { get; set; } = [];
    public bool? FixedWidth { get; set; }
    public double? AIPanicLine { get; set; }
    public double? AIGroundLine { get; set; }
    public int? ShadowTint { get; set; }

    public void Deserialize(XElement e)
    {
        LevelName = e.GetAttribute("LevelName");
        DevOnly = e.GetBoolAttribute("DevOnly");
        TestLevel = e.GetBoolAttribute("TestLevel");

        DisplayName = e.GetElementValue("DisplayName")!;
        LevelID = Utils.ParseIntOrNull(e.GetElementValue("LevelID")) ?? 0;
        TeamColorOrder =
        [
            .. e.GetElementValue("TeamColorOrder")?.Split(',')
                .Select((s) => Utils.ParseEnumOrDefault<TeamColorEnum>(s))
                ?? DEFAULT_TEAM_COLOR_ORDER
        ];
        AvoidTeamColor = Utils.ParseEnumOrDefault<TeamColorEnum>(e.GetElementValue("AvoidTeamColor"));
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
        StreamerBGMusic = e.GetElementValue("StreamerBGMusic");
        ThumbnailPNGFile = e.GetElementValue("ThumbnailPNGFile");
        AIStrictRecover = Utils.ParseBoolOrNull(e.GetElementValue("AIStrictRecover"));
        MidgroundTint = Utils.ParseUIntOrNull(e.GetElementValue("MidgroundTint"));
        MidgroundOffset = Utils.ParseUIntOrNull(e.GetElementValue("MidgroundOffset"));
        MidgroundFraction = Utils.ParseDoubleOrNull(e.GetElementValue("MidgroundFraction"));
        BotTint = Utils.ParseUIntOrNull(e.GetElementValue("BotTint"));
        BotOffset = Utils.ParseUIntOrNull(e.GetElementValue("BotOffset"));
        BotFraction = Utils.ParseDoubleOrNull(e.GetElementValue("BotFraction"));
        ShowPlatsDuringMove = Utils.ParseBoolOrNull(e.GetElementValue("ShowPlatsDuringMove"));
        ShowLavaLevelDuringMove = Utils.ParseBoolOrNull(e.GetElementValue("ShowLavaLevelDuringMove"));
        NegateOverlaps = Utils.ParseBoolOrNull(e.GetElementValue("NegateOverlaps"));
        StartFrame = Utils.ParseIntOrNull(e.GetElementValue("StartFrame"));
        FixedCamera = Utils.ParseBoolOrNull(e.GetElementValue("FixedCamera"));
        AllowItemSpawnOverlap = Utils.ParseBoolOrNull(e.GetElementValue("AllowItemSpawnOverlap"));
        ColorExclusionList = e.GetElementValue("ColorExclusionList")?.Split(',') ?? [];
        FixedWidth = Utils.ParseBoolOrNull(e.GetElementValue("FixedWidth"));
        AIPanicLine = Utils.ParseDoubleOrNull(e.GetElementValue("AIPanicLine"));
        AIGroundLine = Utils.ParseDoubleOrNull(e.GetElementValue("AIGroundLine"));
        ShadowTint = Utils.ParseIntOrNull(e.GetElementValue("ShadowTint"));
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelName", LevelName);
        e.SetAttributeValue("DevOnly", DevOnly);
        e.SetAttributeValue("TestLevel", TestLevel);

        e.AddChild("DisplayName", DisplayName);
        e.AddChild("LevelID", LevelID);
        if (!TeamColorOrder.SequenceEqual(DEFAULT_TEAM_COLOR_ORDER))
            e.AddChild("TeamColorOrder", string.Join(',', TeamColorOrder));
        if (AvoidTeamColor != TeamColorEnum.Default)
            e.AddChild("AvoidTeamColor", AvoidTeamColor);
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
            e.AddChild("HardLeftKill", (bool)HardLeftKill && LeftKill >= 200);
        if (HardRightKill is not null)
            e.AddChild("HardRightKill", (bool)HardRightKill && RightKill >= 200);

        e.AddIfNotNull("BGMusic", BGMusic);
        e.AddIfNotNull("StreamerBGMusic", StreamerBGMusic);
        e.AddIfNotNull("FixedWidth", FixedWidth?.ToString()?.ToUpperInvariant());
        e.AddIfNotNull("ThumbnailPNGFile", ThumbnailPNGFile);
        e.AddIfNotNull("AIStrictRecover", AIStrictRecover?.ToString()?.ToUpperInvariant());

        if (MidgroundTint is not null)
            e.AddChild("MidgroundTint", "0x" + MidgroundTint?.ToString("X6"));
        if (MidgroundOffset is not null)
            e.AddChild("MidgroundOffset", "0x" + MidgroundTint?.ToString("X6"));
        e.AddIfNotNull("MidgroundFraction", MidgroundFraction);

        if (BotTint == 0) e.AddChild("BotTint", 0);
        else if (BotTint > 0) e.AddChild("BotTint", "0x" + BotTint?.ToString("X6"));
        if (LevelName == TEMPLATE_LEVEL_TYPE) e.AddChild("BotOffset", 0);
        else if (BotOffset is not null) e.AddChild("BotOffset", "0x" + BotOffset?.ToString("X6"));
        e.AddIfNotNull("BotFraction", BotFraction);

        e.AddIfNotNull("ShowPlatsDuringMove", ShowPlatsDuringMove);
        // if ShowPlatsDuringMove is true, ShowLavaLevelDuringMove is redundant
        if(ShowPlatsDuringMove == false)
            e.AddIfNotNull("ShowLavaLevelDuringMove", ShowLavaLevelDuringMove);
        e.AddIfNotNull("NegateOverlaps", NegateOverlaps);
        e.AddIfNotNull("StartFrame", StartFrame);
        e.AddIfNotNull("FixedCamera", FixedCamera);
        e.AddIfNotNull("AllowItemSpawnOverlap", AllowItemSpawnOverlap);

        if (ColorExclusionList.Length > 0)
            e.AddChild("ColorExclusionList", string.Join(',', ColorExclusionList));

        e.AddIfNotNull("AIGroundLine", AIGroundLine);
        e.AddIfNotNull("AIPanicLine", AIPanicLine);

        if (ShadowTint is not null)
            e.AddChild("ShadowTint", ShadowTint);
    }
}