using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Gfx : IDeserializable, ISerializable
{
    public enum AsymmetrySwapFlagEnum
    {
        HAND = 1,
        FOREARM = 2,
        ARM = 3,
        SHOULDER = 4,
        LEG = 5,
        SHIN = 6,
        FOOT = 7,
        // 8 isn't settable through xml. it is TORSO.
        GAUNTLETHAND = 9,
        GAUNTLETFOREARM = 10,
        PISTOL = 11,
        KATAR = 12,
        JAW = 13,
        EYES = 14
        // 15 isn't settable through xml. might be BOOTS.
        // 16 isn't settable through xml. it is MOUTH.
        // 17 isn't settable through xml. it is HAIR.
    }

    public string AnimFile { get; set; } = "";
    public string AnimClass { get; set; } = "a__Animation";
    public double AnimScale { get; set; } = 1;
    public double MoveAnimSpeed { get; set; } = 1;
    public string BaseAnim { get; set; } = "Ready";
    public string RunAnim { get; set; } = "Run";
    public bool FlipAnim { get; set; } = false;
    public bool FireAndForget { get; set; } = false;
    public bool RandomFrameStart { get; set; } = false;
    public bool Desynch { get; set; } = false; //yes it's actually called Desynch
    public bool IgnoreCachedWeapon { get; set; } = false;
    public uint Tint { get; set; } = 0; //packed
    public AsymmetrySwapFlagEnum[] AsymmetrySwapFlags { get; set; } = [];
    public CustomArt[] CustomArts { get; set; } = [];
    public ColorSwap[] ColorSwaps { get; set; } = [];

    // these are set in costumeTypes.csv.
    public bool UseRightTorso { get; set; } = false;
    public bool UseRightJaw { get; set; } = false;
    public bool UseRightEyes { get; set; } = false;
    public bool UseRightMouth { get; set; } = false;
    public bool UseRightHair { get; set; } = false;
    public bool UseRightForearm { get; set; } = false;
    public bool UseTrueLeftRightHands { get; set; } = false;
    public Dictionary<string, string> BoneOverrides { get; set; } = [];
    // these are set in weaponSkinType.csv
    public bool UseRightGauntlet { get; set; } = false;
    public bool UseRightKatar { get; set; } = false;

    public void Deserialize(XElement e)
    {
        AnimFile = e.GetElementValue("AnimFile") ?? "";
        AnimClass = e.GetElementValue("AnimClass") ?? "a__Animation";
        AnimScale = Utils.ParseFloatOrNull(e.GetElementValue("AnimScale")) ?? 1;
        MoveAnimSpeed = Utils.ParseFloatOrNull(e.GetElementValue("MoveAnimSpeed")) ?? 1;
        BaseAnim = e.GetElementValue("BaseAnim") ?? "Ready";
        RunAnim = e.GetElementValue("RunAnim") ?? "Run";
        FlipAnim = Utils.ParseBoolOrNull(e.GetElementValue("FlipAnim")) ?? false;
        FireAndForget = Utils.ParseBoolOrNull(e.GetElementValue("FireAndForget")) ?? false;
        RandomFrameStart = Utils.ParseBoolOrNull(e.GetElementValue("RandomFrameStart")) ?? false;
        Desynch = Utils.ParseBoolOrNull(e.GetElementValue("Desynch")) ?? false;
        IgnoreCachedWeapon = Utils.ParseBoolOrNull(e.GetElementValue("IgnoreCachedWeapon")) ?? false;
        Tint = Utils.ParseUIntOrNull(e.GetElementValue("Tint")) ?? 0;

        AsymmetrySwapFlags =
            [.. e.GetElementValue("AsymmetrySwapFlags")?.Split(',')
            .Select(Enum.Parse<AsymmetrySwapFlagEnum>) ?? []];
        CustomArts = [.. e.Elements()
            .Where(e => e.Name.LocalName.StartsWith("CustomArt"))
            .Select(e => e.DeserializeTo<CustomArt>())];
        ColorSwaps = [.. e.Elements()
            .Where(e => e.Name.LocalName.StartsWith("ColorSwap"))
            .Select(e => e.DeserializeTo<ColorSwap>())];
    }

    public void Serialize(XElement e)
    {
        e.Add(new XElement("AnimFile", AnimFile));
        e.Add(new XElement("AnimClass", AnimClass));
        if (AnimScale != 1)
            e.Add(new XElement("AnimScale", AnimScale));
        if (MoveAnimSpeed != 1)
            e.Add(new XElement("MoveAnimSpeed", MoveAnimSpeed));
        if (BaseAnim != "Ready")
            e.Add(new XElement("BaseAnim", BaseAnim));
        if (RunAnim != "Run")
            e.Add(new XElement("RunAnim", RunAnim));
        if (FlipAnim)
            e.Add(new XElement("FlipAnim", FlipAnim));
        if (FireAndForget)
            e.Add(new XElement("FireAndForget", FireAndForget));
        if (RandomFrameStart)
            e.Add(new XElement("RandomFrameStart", RandomFrameStart));
        if (Desynch)
            e.Add(new XElement("Desynch", Desynch));
        if (IgnoreCachedWeapon)
            e.Add(new XElement("IgnoreCachedWeapon", IgnoreCachedWeapon));
        if (Tint != 0)
            e.Add(new XElement("Tint", Tint));
        if (AsymmetrySwapFlags.Length != 0)
            e.Add(new XElement("AsymmetrySwapFlags", string.Join(',', AsymmetrySwapFlags)));
        e.AddManySerialized(CustomArts);
        e.AddManySerialized(ColorSwaps);
    }
}