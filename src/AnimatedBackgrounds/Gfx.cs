using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Gfx : IDeserializable
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

    public string AnimFile{get; set;} = "";
    public string AnimClass{get; set;} = "";
    public double AnimScale{get; set;}
    public double MoveAnimSpeed{get; set;}
    public string BaseAnim{get; set;} = "";
    public string RunAnim{get; set;} = "";
    public bool FlipAnim{get; set;}
    public bool FireAndForget{get; set;}
    public bool RandomFrameStart{get; set;}
    public bool Desynch{get; set;} //yes it's actually called Desynch
    public bool IgnoreCachedWeapon{get; set;}
    public uint Tint{get; set;} //packed
    public uint AsymmetrySwapFlags{get; set;} //packed
    public List<CustomArt> CustomArts{get; set;} = new();
    public List<ColorSwap> ColorSwaps{get; set;} = new();

    public void Deserialize(XElement element)
    {
        AnimFile = element.Element("AnimFile")?.Value ?? "";
        AnimClass = element.Element("AnimClass")?.Value ?? "a__Animation";
        AnimScale = Math.Abs(Utils.ParseFloatOrNull(element.Element("AnimScale")?.Value) ?? 1);
        MoveAnimSpeed = Utils.ParseFloatOrNull(element.Element("MoveAnimSpeed")?.Value) ?? 1;
        BaseAnim = element.Element("BaseAnim")?.Value ?? "Ready";
        RunAnim = element.Element("RunAnim")?.Value ?? "Run";
        FlipAnim = Utils.ParseBoolOrNull(element.Element("FlipAnim")?.Value) ?? false;
        FireAndForget = Utils.ParseBoolOrNull(element.Element("FireAndForget")?.Value) ?? false;
        RandomFrameStart = Utils.ParseBoolOrNull(element.Element("RandomFrameStart")?.Value) ?? false;
        Desynch = Utils.ParseBoolOrNull(element.Element("Desynch")?.Value) ?? false;
        IgnoreCachedWeapon = Utils.ParseBoolOrNull(element.Element("IgnoreCachedWeapon")?.Value) ?? false;
        Tint = Utils.ParseUIntOrNull(element.Element("Tint")?.Value) ?? 0;
        
        AsymmetrySwapFlags = 0;
        string[]? flags = element.Element("AsymmetrySwapFlags")?.Value.Split(',');
        if(flags is not null) foreach(string flag in flags)
        {
            AsymmetrySwapFlagEnum Flag;
            if(!Enum.TryParse<AsymmetrySwapFlagEnum>(flag, out Flag))
            {
                //TODO: log
            }
            else
            {
                AsymmetrySwapFlags |= (1u << (int)Flag);
            }
        }

        CustomArts = element.Elements()
            .Where(e => e.Name.LocalName.StartsWith("CustomArt"))
            .Select(e => e.DeserializeTo<CustomArt>())
            .ToList();
        
        ColorSwaps = element.Elements()
            .Where(e => e.Name.LocalName.StartsWith("ColorSwap"))
            .Select(e => e.DeserializeTo<ColorSwap>())
            .ToList();
    }
}