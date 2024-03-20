using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnimation : IDeserializable, ISerializable
{
    public int Delay { get; set; }
    public int Interval { get; set; }
    public int IntervalRand { get; set; }
    public List<string> AnimationName { get; set; } = null!;
    public string FileName { get; set; } = null!;
    // why is this a string
    public string PositionX { get; set; } = null!;
    // why is this a string
    public string PositionY { get; set; } = null!;
    public string PlatID { get; set; } = null!;
    public double Scale { get; set; }
    public double RandX { get; set; }
    public double RandY { get; set; }
    public bool Flip { get; set; }
    public bool PlayForeground { get; set; }
    public bool PlayMidground { get; set; }
    public bool PlayBackground { get; set; }
    public bool IgnoreOnBlurBG { get; set; }

    public void Deserialize(XElement e)
    {
        Delay = e.GetIntAttribute("Delay", 0);
        Interval = e.GetIntAttribute("Interval", 0);
        IntervalRand = e.GetIntAttribute("IntervalRand", 0);
        AnimationName = [.. e.GetAttribute("AnimationName").Split(',')];
        FileName = e.GetAttribute("FileName");
        PositionX = e.GetAttribute("PositionX");
        PositionY = e.GetAttribute("PositionY");
        PlatID = e.GetAttribute("PlatID");
        Scale = e.GetFloatAttribute("Scale", 0); // yes, this defaults to 0
        RandX = e.GetFloatAttribute("Scale", 0);
        RandY = e.GetFloatAttribute("Scale", 0);
        Flip = e.GetBoolAttribute("Flip", false);
        PlayForeground = e.GetBoolAttribute("PlayForeground", false);
        PlayMidground = e.GetBoolAttribute("PlayMidground", false);
        PlayBackground = e.GetBoolAttribute("PlayBackground", false);
        IgnoreOnBlurBG = e.GetBoolAttribute("IgnoreOnBlurBG", false);
    }

    public void Serialize(XElement e)
    {
        if (Delay != 0)
            e.SetAttributeValue("Delay", Delay);
        if (Interval != 0)
            e.SetAttributeValue("Interval", Interval);
        if (IntervalRand != 0)
            e.SetAttributeValue("IntervalRand", IntervalRand);
        e.SetAttributeValue("AnimationName", string.Join(',', AnimationName));
        e.SetAttributeValue("FileName", FileName);
        e.SetAttributeValue("PositionX", PositionX);
        e.SetAttributeValue("PositionY", PositionY);
        e.SetAttributeValue("PlatID", PlatID);
        if (Scale != 0)
            e.SetAttributeValue("Scale", Scale);
        if (RandX != 0)
            e.SetAttributeValue("RandX", RandX);
        if (RandY != 0)
            e.SetAttributeValue("RandY", RandY);
        if (PlayForeground)
            e.SetAttributeValue("PlayForeground", PlayForeground);
        if (PlayMidground)
            e.SetAttributeValue("PlayMidground", PlayMidground);
        if (PlayBackground)
            e.SetAttributeValue("PlayBackground", PlayBackground);
        if (IgnoreOnBlurBG)
            e.SetAttributeValue("IgnoreOnBlurBG", IgnoreOnBlurBG);
    }
}