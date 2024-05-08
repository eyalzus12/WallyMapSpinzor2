using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnimation : IDeserializable, ISerializable
{
    public List<string> AnimationName { get; set; } = null!;
    public bool PlayMidground { get; set; }
    public bool Flip { get; set; }
    public string FileName { get; set; } = null!;
    public bool PlayForeground { get; set; }
    public bool PlayBackground { get; set; }
    public int InitDelay { get; set; }
    public int Interval { get; set; }
    public int IntervalRand { get; set; }
    // why is this a string
    public string PositionX { get; set; } = null!;
    // why is this a string
    public string PositionY { get; set; } = null!;
    public double Rotation { get; set; }
    public double Scale { get; set; }
    public double RandX { get; set; }
    public double RandY { get; set; }
    public int LoopIterations { get; set; }
    public string PlatID { get; set; } = null!;
    public bool IgnoreOnBlurBG { get; set; }

    public void Deserialize(XElement e)
    {
        AnimationName = [.. e.GetAttribute("AnimationName").Split(',')];
        PlayMidground = e.GetBoolAttribute("PlayMidground", false);
        Flip = e.GetBoolAttribute("Flip", false);
        FileName = e.GetAttribute("FileName");
        PlayForeground = e.GetBoolAttribute("PlayForeground", false);
        PlayBackground = e.GetBoolAttribute("PlayBackground", false);
        InitDelay = e.GetIntAttribute("InitDelay", 0);
        Interval = e.GetIntAttribute("Interval", 0);
        IntervalRand = e.GetIntAttribute("IntervalRand", 0);
        PositionX = e.GetAttribute("PositionX");
        PositionY = e.GetAttribute("PositionY");
        Rotation = e.GetFloatAttribute("Rotation", 0);
        Scale = e.GetFloatAttribute("Scale", 0); // yes, this defaults to 0
        RandX = e.GetFloatAttribute("RandX", 0);
        RandY = e.GetFloatAttribute("RandY", 0);
        LoopIterations = e.GetIntAttribute("LoopIterations", 0);
        PlatID = e.GetAttribute("PlatID");
        IgnoreOnBlurBG = e.GetBoolAttribute("IgnoreOnBlurBG", false);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("AnimationName", string.Join(',', AnimationName));
        if (PlayMidground)
            e.SetAttributeValue("PlayMidground", PlayMidground);
        if (Flip)
            e.SetAttributeValue("Flip", Flip);
        e.SetAttributeValue("FileName", FileName);
        if (PlayForeground)
            e.SetAttributeValue("PlayForeground", PlayForeground);
        if (PlayBackground)
            e.SetAttributeValue("PlayBackground", PlayBackground);
        if (InitDelay != 0)
            e.SetAttributeValue("InitDelay", InitDelay);
        if (Interval != 0)
            e.SetAttributeValue("Interval", Interval);
        if (IntervalRand != 0)
            e.SetAttributeValue("IntervalRand", IntervalRand);
        e.SetAttributeValue("PositionX", PositionX);
        e.SetAttributeValue("PositionY", PositionY);
        if (Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation);
        if (Scale != 0)
            e.SetAttributeValue("Scale", Scale);
        if (RandX != 0)
            e.SetAttributeValue("RandX", RandX);
        if (RandY != 0)
            e.SetAttributeValue("RandY", RandY);
        if (LoopIterations != 0)
            e.SetAttributeValue("LoopIterations", LoopIterations);
        e.SetAttributeValue("PlatID", PlatID);
        if (IgnoreOnBlurBG)
            e.SetAttributeValue("IgnoreOnBlurBG", IgnoreOnBlurBG);
    }
}