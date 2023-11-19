using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelType : IDeserializable, ISerializable
{
    public string LevelName{get; set;} = null!;
    public bool DevOnly{get; set;}
    public bool TestLevel{get; set;}

    public string DisplayName{get; set;} = null!;
    public int LevelID{get; set;} //max 255
    public string? FileName{get; set;} = null!;
    public string? AssetName{get; set;} = null!;
    public CrateColor? CrateColorA{get; set;}
    public CrateColor? CrateColorB{get; set;} 

    //only null for random LevelType
    public int? LeftKill{get; set;} 
    public int? RightKill{get; set;}
    public int? TopKill{get; set;}
    public int? BottomKill{get; set;}

    public bool? SoftTopKill{get; set;}
    public bool? HardLeftKill{get; set;} //LeftKill needs to be equal or more than 200 for this to be true
    public bool? HardRightKill{get; set;} //RightKill needs to be equal or more than 200 for this to be true
    public string? BGMusic{get; set;} = null!;
    public string ThumbnailPNGFile{get; set;} = null!;
    public bool? AIStrictRecover{get; set;}

    //used nowhere except in the template
    public uint? MidgroundTint{get; set;}
    public uint? MidgroundOffset{get; set;}
    public double? MidgroundFraction{get; set;}

    public uint? BotTint{get; set;} //always 0 in xml
    public uint? BotOffset{get; set;}
    public double? BotFraction{get; set;}

    public bool? ShowPlatsDuringMove{get; set;}
    public bool? NegateOverlaps{get; set;}
    public int? StartFrame{get; set;}
    public bool? FixedCamera{get; set;}
    public bool? AllowItemSpawnOverlap{get; set;}
    public List<string> ColorExclusionList{get; set;} = null!;
    public bool? FixedWidth{get; set;}
    public double? AIPanicLine{get; set;}
    public double? AIGroundLine{get; set;}

    //not used in LevelTypes.xml, but they exist in the code
    public uint? ShadowTint{get; set;}
    //public idk ItemOverride{get; set;}

    public void Deserialize(XElement e)
    {
        LevelName = e.GetAttribute("LevelName");
        DevOnly = e.GetBoolAttribute("DevOnly");
        TestLevel = e.GetBoolAttribute("TestLevel");

        DisplayName = e.Element("DisplayName")!.Value;
        LevelID = Utils.ParseIntOrNull(e.Element("LevelID")?.Value) ?? 0;
        FileName = e.Element("FileName")?.Value;
        AssetName = e.Element("AssetName")?.Value;

        uint? colA = Utils.ParseUIntOrNull(e.Element("CrateColorA")?.Value);
        CrateColorA = colA is null ? null : CrateColor.FromHex((uint)colA);
        uint? colB = Utils.ParseUIntOrNull(e.Element("CrateColorB")?.Value);
        CrateColorB = colB is null ? null : CrateColor.FromHex((uint)colB);

        LeftKill = Utils.ParseIntOrNull(e.Element("LeftKill")?.Value);
        RightKill = Utils.ParseIntOrNull(e.Element("RightKill")?.Value);
        TopKill = Utils.ParseIntOrNull(e.Element("TopKill")?.Value);
        BottomKill = Utils.ParseIntOrNull(e.Element("BottomKill")?.Value);
        SoftTopKill = Utils.ParseBoolOrNull(e.Element("SoftTopKill")?.Value);
        HardLeftKill = Utils.ParseBoolOrNull(e.Element("HardLeftKill")?.Value);
        HardRightKill = Utils.ParseBoolOrNull(e.Element("HardRightKill")?.Value);
        BGMusic = e.Element("BGMusic")?.Value;
        ThumbnailPNGFile = e.Element("ThumbnailPNGFile")?.Value ?? "CorruptFile.png";
        AIStrictRecover = Utils.ParseBoolOrNull(e.Element("AIStrictRecover")?.Value);
        MidgroundTint = Utils.ParseUIntOrNull(e.Element("MidgroundTint")?.Value);
        MidgroundOffset = Utils.ParseUIntOrNull(e.Element("MidgroundOffset")?.Value);
        MidgroundFraction = Utils.ParseFloatOrNull(e.Element("MidgroundFraction")?.Value);
        BotTint = Utils.ParseUIntOrNull(e.Element("BotTint")?.Value);
        BotOffset = Utils.ParseUIntOrNull(e.Element("BotOffset")?.Value);
        BotFraction = Utils.ParseFloatOrNull(e.Element("BotFraction")?.Value);
        ShowPlatsDuringMove = Utils.ParseBoolOrNull(e.Element("ShowPlatsDuringMove")?.Value);
        NegateOverlaps = Utils.ParseBoolOrNull(e.Element("NegateOverlaps")?.Value);
        StartFrame = Utils.ParseIntOrNull(e.Element("StartFrame")?.Value);
        FixedCamera = Utils.ParseBoolOrNull(e.Element("FixedCamera")?.Value);
        AllowItemSpawnOverlap = Utils.ParseBoolOrNull(e.Element("AllowItemSpawnOverlap")?.Value);
        ColorExclusionList = e.Element("ColorExclusionList")?.Value.Split(",").ToList() ?? new();
        FixedWidth = Utils.ParseBoolOrNull(e.Element("FixedWidth")?.Value);
        AIPanicLine = Utils.ParseFloatOrNull(e.Element("AIPanicLine")?.Value);
        AIGroundLine = Utils.ParseFloatOrNull(e.Element("AIGroundLine")?.Value);
        ShadowTint = Utils.ParseUIntOrNull(e.Element("ShadowTint")?.Value);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("LevelName", LevelName);
        e.SetAttributeValue("DevOnly", DevOnly);
        e.SetAttributeValue("TestLevel", TestLevel);

        e.Add(new XElement("DisplayName", DisplayName));
        e.Add(new XElement("LevelID", LevelID));
        e.AddIfNotNull("FileName", FileName);
        e.AddIfNotNull("AssetName", AssetName);
        e.AddIfNotNull("CrateColorA", CrateColorA?.ToHexString());
        e.AddIfNotNull("CrateColorB", CrateColorB?.ToHexString());
        e.AddIfNotNull("LeftKill", LeftKill);
        e.AddIfNotNull("RightKill", RightKill);
        e.AddIfNotNull("TopKill", TopKill);
        e.AddIfNotNull("BottomKill", BottomKill);
        e.AddIfNotNull("SoftTopKill", SoftTopKill);

        if(HardLeftKill is not null)
            e.Add(new XElement("HardLeftKill", (bool)HardLeftKill && LeftKill >= 200));
        if(HardRightKill is not null)
            e.Add(new XElement("HardRightKill", (bool)HardRightKill && RightKill >= 200));

        e.AddIfNotNull("BGMusic", BGMusic);
        e.AddIfNotNull("FixedWidth", FixedWidth);
        e.AddIfNotNull("ThumbnailPNGFile", ThumbnailPNGFile);
        e.AddIfNotNull("AIStrictRecover", AIStrictRecover);

        if(MidgroundTint == 0) e.Add(new XElement("MidgroundTint", 0));
        else if(MidgroundTint > 0) e.Add(new XElement("MidgroundTint", "0x" + MidgroundTint?.ToString("X6")));
        if(MidgroundOffset == 0) e.Add(new XElement("MidgroundOffset", 0));
        else if(MidgroundOffset > 0) e.Add(new XElement("MidgroundOffset", "0x" + MidgroundTint?.ToString("X6")));
        e.AddIfNotNull("MidgroundFraction", MidgroundFraction);
        
        if(BotTint == 0) e.Add(new XElement("BotTint", 0));
        else if(BotTint > 0) e.Add(new XElement("BotTint", "0x" + BotTint?.ToString("X6")));
        if(BotOffset== 0) e.Add(new XElement("BotOffset", 0));
        else if(BotOffset > 0) e.Add(new XElement("BotOffset", "0x" + BotOffset?.ToString("X6")));
        e.AddIfNotNull("BotFraction", BotFraction);

        e.AddIfNotNull("ShowPlatsDuringMove", ShowPlatsDuringMove);
        e.AddIfNotNull("NegateOverlaps", NegateOverlaps);
        e.AddIfNotNull("StartFrame", StartFrame);
        e.AddIfNotNull("FixedCamera", FixedCamera);
        e.AddIfNotNull("AllowItemSpawnOverlap", AllowItemSpawnOverlap);
        
        if(ColorExclusionList.Count > 0)
            e.Add(new XElement("ColorExclusionList", string.Join(',', ColorExclusionList)));

        e.AddIfNotNull("AIGroundLine", AIGroundLine);
        e.AddIfNotNull("AIPanicLine", AIPanicLine);

        if(ShadowTint == 0) e.Add(new XElement("ShadowTint", ShadowTint)); 
        else if(ShadowTint > 0) e.Add(new XElement("ShadowTint", "0x" + ShadowTint?.ToString("X6")));
    }
}