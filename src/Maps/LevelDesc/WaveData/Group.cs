using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Group : IDeserializable, ISerializable
{
    public int? Count{get; set;}
    public int? Count3{get; set;}
    public int? Count4{get; set;}
    public int? Delay{get; set;}
    public int? Delay3{get; set;}
    public int? Delay4{get; set;}
    public int? Stagger{get; set;}
    public int? Stagger3{get; set;}
    public int? Stagger4{get; set;}
    public DirEnum Dir{get; set;}
    public PathEnum Path{get; set;}
    public BehaviorEnum Behavior{get; set;}
    public bool Shared{get; set;}
    public bool SharedPath{get; set;}

    public void Deserialize(XElement e)
    {
        Count = e.GetNullableIntAttribute("Count");
        Count3 = e.GetNullableIntAttribute("Count3");
        Count4 = e.GetNullableIntAttribute("Count4");
        Delay = e.GetNullableIntAttribute("Delay");
        Delay3 = e.GetNullableIntAttribute("Delay3");
        Delay4 = e.GetNullableIntAttribute("Delay4");
        Stagger = e.GetNullableIntAttribute("Stagger");
        Stagger3 = e.GetNullableIntAttribute("Stagger3");
        Stagger4 = e.GetNullableIntAttribute("Stagger4");
        Dir = MapUtils.ParseDir(e.GetNullableAttribute("Dir"));
        Path = MapUtils.ParsePath(e.GetNullableAttribute("Path"));
        Behavior = MapUtils.ParseBehavior(e.GetNullableAttribute("Behavior"));
        Shared = MapUtils.IsSharedDir(Dir) || e.GetBoolAttribute("Shared", false);
        SharedPath = MapUtils.IsSharedPath(Path) || e.GetBoolAttribute("SharedPath", false);
    }

    public void Serialize(XElement e)
    {
        if(Count is not null)
            e.SetAttributeValue("Count", Count.ToString());
        if(Count3 is not null)
            e.SetAttributeValue("Count3", Count3.ToString());
        if(Count4 is not null)
            e.SetAttributeValue("Count4", Count4.ToString());
        
        if(Delay is not null)
            e.SetAttributeValue("Delay", Delay.ToString());
        if(Delay3 is not null)
            e.SetAttributeValue("Delay3", Delay3.ToString());
        if(Delay4 is not null)
            e.SetAttributeValue("Delay4", Delay4.ToString());
        
        if(Stagger is not null)
            e.SetAttributeValue("Stagger", Stagger.ToString());
        if(Stagger3 is not null)
            e.SetAttributeValue("Stagger3", Stagger3.ToString());
        if(Stagger4 is not null)
            e.SetAttributeValue("Stagger4", Stagger4.ToString());
        
        if(Dir != DirEnum.ANY)
            e.SetAttributeValue("Dir", Dir.ToString().ToUpper());
        if(Path != PathEnum.ANY)
            e.SetAttributeValue("Path", Path.ToString().ToUpper());
        if(Behavior != BehaviorEnum.NORMAL)
            e.SetAttributeValue("Behavior", Behavior.ToString().ToUpper());
        if(!MapUtils.IsSharedDir(Dir) && Shared)
            e.SetAttributeValue("Shared", Shared.ToString().ToUpper());
        if(!MapUtils.IsSharedPath(Path) && SharedPath)
            e.SetAttributeValue("SharedPath", SharedPath.ToString().ToUpper());
    }
}