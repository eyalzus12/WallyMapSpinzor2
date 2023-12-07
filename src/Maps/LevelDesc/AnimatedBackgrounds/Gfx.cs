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
        //8 is missing?
        GAUNTLETHAND = 9,
        GAUNTLETFOREARM = 10,
        PISTOL = 11,
        KATAR = 12,
        JAW = 13,
        EYES = 14
    }

    public string AnimFile{get; set;} = null!;
    public string AnimClass{get; set;} = null!;
    public double AnimScale{get; set;}
    public double MoveAnimSpeed{get; set;}
    public string BaseAnim{get; set;} = null!;
    public string RunAnim{get; set;} = null!;
    public bool FlipAnim{get; set;}
    public bool FireAndForget{get; set;}
    public bool RandomFrameStart{get; set;}
    public bool Desynch{get; set;} //yes it's actually called Desynch
    public bool IgnoreCachedWeapon{get; set;}
    public uint Tint{get; set;} //packed
    public List<AsymmetrySwapFlagEnum> AsymmetrySwapFlags{get; set;} = null!;
    public List<CustomArt> CustomArts{get; set;} = null!;
    public List<ColorSwap> ColorSwaps{get; set;} = null!;

    public void Deserialize(XElement e)
    {
        AnimFile = e.Element("AnimFile")?.Value ?? "";
        AnimClass = e.Element("AnimClass")?.Value ?? "a__Animation";
        AnimScale = Utils.ParseFloatOrNull(e.Element("AnimScale")?.Value) ?? 1;
        MoveAnimSpeed = Utils.ParseFloatOrNull(e.Element("MoveAnimSpeed")?.Value) ?? 1;
        BaseAnim = e.Element("BaseAnim")?.Value ?? "Ready";
        RunAnim = e.Element("RunAnim")?.Value ?? "Run";
        FlipAnim = Utils.ParseBoolOrNull(e.Element("FlipAnim")?.Value) ?? false;
        FireAndForget = Utils.ParseBoolOrNull(e.Element("FireAndForget")?.Value) ?? false;
        RandomFrameStart = Utils.ParseBoolOrNull(e.Element("RandomFrameStart")?.Value) ?? false;
        Desynch = Utils.ParseBoolOrNull(e.Element("Desynch")?.Value) ?? false;
        IgnoreCachedWeapon = Utils.ParseBoolOrNull(e.Element("IgnoreCachedWeapon")?.Value) ?? false;
        Tint = Utils.ParseUIntOrNull(e.Element("Tint")?.Value) ?? 0;
        
        AsymmetrySwapFlags =
            e.Element("AsymmetrySwapFlags")?.Value.Split(',')
            .Select(Enum.Parse<AsymmetrySwapFlagEnum>).ToList() ?? new();

        CustomArts = e.Elements()
            .Where(e => e.Name.LocalName.StartsWith("CustomArt"))
            .Select(e => e.DeserializeTo<CustomArt>())
            .ToList();
        
        ColorSwaps = e.Elements()
            .Where(e => e.Name.LocalName.StartsWith("ColorSwap"))
            .Select(e => e.DeserializeTo<ColorSwap>())
            .ToList();
    }

    public void Serialize(XElement e)
    {
        e.Add(new XElement("AnimFile", AnimFile));
        e.Add(new XElement("AnimClass", AnimClass));
        if(AnimScale != 1)
            e.Add(new XElement("AnimScale", AnimScale.ToString()));
        if(MoveAnimSpeed != 1)
            e.Add(new XElement("MoveAnimSpeed", MoveAnimSpeed.ToString()));
        if(BaseAnim != "Ready")
            e.Add(new XElement("BaseAnim", BaseAnim));
        if(RunAnim != "Run")
            e.Add(new XElement("RunAnim", RunAnim));
        if(FlipAnim)
            e.Add(new XElement("FlipAnim", FlipAnim));
        if(FireAndForget)
            e.Add(new XElement("FireAndForget", FireAndForget));
        if(RandomFrameStart)
            e.Add(new XElement("RandomFrameStart", RandomFrameStart));
        if(Desynch)
            e.Add(new XElement("Desynch", Desynch));
        if(IgnoreCachedWeapon)
            e.Add(new XElement("IgnoreCachedWeapon", IgnoreCachedWeapon));
        if(Tint != 0)
            e.Add(new XElement("Tint", Tint));
        if(AsymmetrySwapFlags.Count != 0)
            e.Add(new XElement("AsymmetrySwapFlags", string.Join(',', AsymmetrySwapFlags)));
        e.AddManySerialized(CustomArts);
        e.AddManySerialized(ColorSwaps);
    }
}