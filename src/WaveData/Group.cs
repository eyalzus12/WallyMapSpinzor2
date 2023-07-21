using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Group : IDeserializable
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
    public Dir Dir{get; set;}
    public Path Path{get; set;}
    public Behavior Behavior{get; set;}
    public bool Shared{get; set;}
    public bool SharedPath{get; set;}

    public void Deserialize(XElement element)
    {
        Count = element.GetNullableIntAttribute("Count");
        Count3 = element.GetNullableIntAttribute("Count3");
        Count4 = element.GetNullableIntAttribute("Count4");
        Delay = element.GetNullableIntAttribute("Delay");
        Delay3 = element.GetNullableIntAttribute("Delay3");
        Delay4 = element.GetNullableIntAttribute("Delay4");
        Stagger = element.GetNullableIntAttribute("Stagger");
        Stagger3 = element.GetNullableIntAttribute("Stagger3");
        Stagger4 = element.GetNullableIntAttribute("Stagger4");
        Dir = Utils.ParseDir(element.GetNullableAttribute("Dir"));
        Path = Utils.ParsePath(element.GetNullableAttribute("Path"));
        Behavior = Utils.ParseBehavior(element.GetNullableAttribute("Behavior"));
        Shared = Utils.IsSharedDir(Dir) || element.GetBoolAttribute("Shared", false);
        SharedPath = Utils.IsSharedPath(Path) || element.GetBoolAttribute("SharedPath", false);
    }
}