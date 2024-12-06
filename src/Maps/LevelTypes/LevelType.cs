using System;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelType : IDeserializable, ISerializable
{
    private const string TEMPLATE_LEVEL_TYPE = "Template";
    private static readonly TeamColorEnum[] DEFAULT_TEAM_COLOR_ORDER = [TeamColorEnum.Red, TeamColorEnum.Blue, TeamColorEnum.Yellow, TeamColorEnum.Purple];

    public string LevelName { get; set; } = null!;
    public bool DevOnly { get; set; }
    public bool TestLevel { get; set; }

    public string DisplayName { get; set; } = null!;
    public uint LevelID { get; set; } //max 255
    public TeamColorEnum[] TeamColorOrder { get; set; } = DEFAULT_TEAM_COLOR_ORDER;
    public TeamColorEnum AvoidTeamColor { get; set; }
    public string? FileName { get; set; }
    public string? AssetName { get; set; }
    public CrateColor? CrateColorA { get; set; }
    public CrateColor? CrateColorB { get; set; }

    //only null for random LevelType
    public uint? LeftKill { get; set; }
    public uint? RightKill { get; set; }
    public uint? TopKill { get; set; }
    public uint? BottomKill { get; set; }

    public bool? SoftTopKill { get; set; }
    public bool? HardLeftKill { get; set; } // LeftKill needs to be equal or more than 200 for this to be true
    public bool? HardRightKill { get; set; } // RightKill needs to be equal or more than 200 for this to be true
    public uint? MinNumOnlineGamesBeforeRandom { get; set; }
    public string? BGMusic { get; set; }
    public string? StreamerBGMusic { get; set; }
    public string? ThumbnailPNGFile { get; set; }
    public bool? AIStrictRecover { get; set; }

    // these 3 are unused
    public uint? MidgroundTint { get; set; }
    public uint? MidgroundOffset { get; set; }
    public double? MidgroundFraction { get; set; } // defaults to 0

    public uint? BotTint { get; set; }
    public uint? BotOffset { get; set; }
    public double? BotFraction { get; set; } // defaults to 0.5

    public bool? ShowPlatsDuringMove { get; set; }
    public bool? ShowLavaLevelDuringMove { get; set; }
    public bool? NegateOverlaps { get; set; }
    public uint? StartFrame { get; set; }
    public bool? IsClimbMap { get; set; }
    public bool? FixedCamera { get; set; }
    public bool? AllowItemSpawnOverlap { get; set; } // unused
    public string[] ColorExclusionList { get; set; } = [];
    public bool? FixedWidth { get; set; }
    public double? AIPanicLine { get; set; }
    public double? AIGroundLine { get; set; }
    public int? ShadowTint { get; set; } // yes, this is an int and not a uint

    public void Deserialize(XElement e)
    {
        LevelName = e.GetAttribute("LevelName");
        DevOnly = e.GetBoolAttribute("DevOnly", false);
        TestLevel = e.GetBoolAttribute("TestLevel", false);

        DisplayName = e.GetElement("DisplayName");
        LevelID = e.GetUIntElement("LevelID", 0);
        TeamColorOrder =
        [
            .. e.GetElementOrNull("TeamColorOrder")?.Split(',')
                .Select((s) => Utils.ParseEnumOrDefault<TeamColorEnum>(s))
                ?? DEFAULT_TEAM_COLOR_ORDER
        ];
        AvoidTeamColor = Utils.ParseEnumOrDefault<TeamColorEnum>(e.GetElementOrNull("AvoidTeamColor"));
        FileName = e.GetElementOrNull("FileName");
        AssetName = e.GetElementOrNull("AssetName");

        uint? colA = e.GetUIntElementOrNull("CrateColorA");
        CrateColorA = colA is null ? null : CrateColor.FromHex(colA.Value);
        uint? colB = e.GetUIntElementOrNull("CrateColorB");
        CrateColorB = colB is null ? null : CrateColor.FromHex(colB.Value);

        LeftKill = e.GetUIntElementOrNull("LeftKill");
        RightKill = e.GetUIntElementOrNull("RightKill");
        TopKill = e.GetUIntElementOrNull("TopKill");
        BottomKill = e.GetUIntElementOrNull("BottomKill");
        SoftTopKill = e.GetBoolElementOrNull("SoftTopKill");
        HardLeftKill = e.GetBoolElementOrNull("HardLeftKill");
        HardRightKill = e.GetBoolElementOrNull("HardRightKill");
        MinNumOnlineGamesBeforeRandom = e.GetUIntElementOrNull("MinNumOnlineGamesBeforeRandom");
        BGMusic = e.GetElementOrNull("BGMusic");
        StreamerBGMusic = e.GetElementOrNull("StreamerBGMusic");
        ThumbnailPNGFile = e.GetElementOrNull("ThumbnailPNGFile");
        AIStrictRecover = e.GetBoolElementOrNull("AIStrictRecover");
        MidgroundTint = e.GetUIntElementOrNull("MidgroundTint");
        MidgroundOffset = e.GetUIntElementOrNull("MidgroundOffset");
        MidgroundFraction = e.GetDoubleElementOrNull("MidgroundFraction");
        BotTint = e.GetUIntElementOrNull("BotTint");
        BotOffset = e.GetUIntElementOrNull("BotOffset");
        BotFraction = e.GetDoubleElementOrNull("BotFraction");
        ShowPlatsDuringMove = e.GetBoolElementOrNull("ShowPlatsDuringMove");
        ShowLavaLevelDuringMove = e.GetBoolElementOrNull("ShowLavaLevelDuringMove");
        NegateOverlaps = e.GetBoolElementOrNull("NegateOverlaps");
        StartFrame = e.GetUIntElementOrNull("StartFrame");
        IsClimbMap = e.GetBoolElementOrNull("IsClimbMap");
        FixedCamera = e.GetBoolElementOrNull("FixedCamera");
        AllowItemSpawnOverlap = e.GetBoolElementOrNull("AllowItemSpawnOverlap");
        ColorExclusionList = e.GetElementOrNull("ColorExclusionList")?.Split(',') ?? [];
        FixedWidth = e.GetBoolElementOrNull("FixedWidth");
        AIPanicLine = e.GetDoubleElementOrNull("AIPanicLine");
        AIGroundLine = e.GetDoubleElementOrNull("AIGroundLine");
        ShadowTint = e.GetIntElementOrNull("ShadowTint");
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
            e.AddChild("HardLeftKill", HardLeftKill.Value && LeftKill >= 200);
        if (HardRightKill is not null)
            e.AddChild("HardRightKill", HardRightKill.Value && RightKill >= 200);

        e.AddIfNotNull("MinNumOnlineGamesBeforeRandom", MinNumOnlineGamesBeforeRandom);
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
        if (ShowPlatsDuringMove != true)
            e.AddIfNotNull("ShowLavaLevelDuringMove", ShowLavaLevelDuringMove);
        e.AddIfNotNull("NegateOverlaps", NegateOverlaps);
        e.AddIfNotNull("StartFrame", StartFrame);
        e.AddIfNotNull("IsClimbMap", IsClimbMap);
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