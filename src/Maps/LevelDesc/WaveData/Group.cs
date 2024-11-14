using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Group : IDeserializable, ISerializable
{
    public uint? Count { get; set; }
    public uint? Count3 { get; set; }
    public uint? Count4 { get; set; }
    public uint? Delay { get; set; }
    public uint? Delay3 { get; set; }
    public uint? Delay4 { get; set; }
    public uint? Stagger { get; set; }
    public uint? Stagger3 { get; set; }
    public uint? Stagger4 { get; set; }
    public DirEnum Dir { get; set; }
    public PathEnum Path { get; set; }
    public BehaviorEnum Behavior { get; set; }
    public bool Shared { get; set; }
    public bool SharedPath { get; set; }

    public WaveData? Parent { get; set; }

    public void Deserialize(XElement e)
    {
        Count = e.GetUIntAttributeOrNull("Count");
        Count3 = e.GetUIntAttributeOrNull("Count3");
        Count4 = e.GetUIntAttributeOrNull("Count4");
        Delay = e.GetUIntAttributeOrNull("Delay");
        Delay3 = e.GetUIntAttributeOrNull("Delay3");
        Delay4 = e.GetUIntAttributeOrNull("Delay4");
        Stagger = e.GetUIntAttributeOrNull("Stagger");
        Stagger3 = e.GetUIntAttributeOrNull("Stagger3");
        Stagger4 = e.GetUIntAttributeOrNull("Stagger4");
        Dir = MapUtils.ParseDir(e.GetAttributeOrNull("Dir"));
        Path = MapUtils.ParsePath(e.GetAttributeOrNull("Path"));
        Behavior = MapUtils.ParseBehavior(e.GetAttributeOrNull("Behavior"));
        Shared = MapUtils.IsSharedDir(Dir) || e.GetBoolAttribute("Shared", false);
        SharedPath = MapUtils.IsSharedPath(Path) || e.GetBoolAttribute("SharedPath", false);
    }

    public void Serialize(XElement e)
    {
        if (Count is not null)
            e.SetAttributeValue("Count", Count);
        if (Count3 is not null)
            e.SetAttributeValue("Count3", Count3);
        if (Count4 is not null)
            e.SetAttributeValue("Count4", Count4);

        if (Delay is not null)
            e.SetAttributeValue("Delay", Delay);
        if (Delay3 is not null)
            e.SetAttributeValue("Delay3", Delay3);
        if (Delay4 is not null)
            e.SetAttributeValue("Delay4", Delay4);

        if (Stagger is not null)
            e.SetAttributeValue("Stagger", Stagger);
        if (Stagger3 is not null)
            e.SetAttributeValue("Stagger3", Stagger3);
        if (Stagger4 is not null)
            e.SetAttributeValue("Stagger4", Stagger4);

        if (Dir != DirEnum.ANY)
            e.SetAttributeValue("Dir", Dir);
        if (Path != PathEnum.ANY)
            e.SetAttributeValue("Path", Path);
        if (Behavior != BehaviorEnum._)
            e.SetAttributeValue("Behavior", Behavior);
        if (!MapUtils.IsSharedDir(Dir) && Shared)
            e.SetAttributeValue("Shared", Shared.ToString().ToUpperInvariant());
        if (!MapUtils.IsSharedPath(Path) && SharedPath)
            e.SetAttributeValue("SharedPath", SharedPath.ToString().ToUpperInvariant());
    }

    public uint GetCount(uint players) => players switch
    {
        >= 4 when Count4 is not null => Count4.Value,
        >= 3 when Count3 is not null => Count3.Value,
        _ when Count is not null => Count.Value,
        _ => 1
    };

    public uint GetDelay(uint players) => players switch
    {
        >= 4 when Delay4 is not null => 100 * (uint)Math.Ceiling(Delay4.Value / 100.0),
        >= 3 when Delay3 is not null => 100 * (uint)Math.Ceiling(Delay3.Value / 100.0),
        _ when Delay is not null => 100 * (uint)Math.Ceiling(Delay.Value / 100.0),
        _ => 0
    };

    public uint GetStagger(uint players) => players switch
    {
        >= 4 when Stagger4 is not null => 100 * (uint)Math.Ceiling(Stagger4.Value / 100.0),
        >= 3 when Stagger3 is not null => 100 * (uint)Math.Ceiling(Stagger3.Value / 100.0),
        _ when Stagger is not null => 100 * (uint)Math.Ceiling(Stagger.Value / 100.0),
        _ => 500
    };
}