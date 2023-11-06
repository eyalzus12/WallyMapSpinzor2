using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class WaveData : IDeserializable, ISerializable
{
    public int ID{get; set;}
    public double? Speed{get; set;}
    public double? Speed3{get; set;}
    public double? Speed4{get; set;}
    public int LoopIdx{get; set;}
    public List<CustomPath> CustomPaths{get; set;} = null!;
    public List<Group> Groups{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        ID = element.GetIntAttribute("ID", 0);
        Speed = element.GetNullableFloatAttribute("Speed");
        Speed3 = element.GetNullableFloatAttribute("Speed3");
        Speed4 = element.GetNullableFloatAttribute("Speed4");
        LoopIdx = element.GetIntAttribute("LoopIdx", 0);
        CustomPaths = element.DeserializeChildrenOfType<CustomPath>();
        Groups = element.DeserializeChildrenOfType<Group>();
    }

    public XElement Serialize()
    {
        XElement e = new("WaveData");

        e.SetAttributeValue("ID", ID.ToString());

        if(Speed is not null)
            e.SetAttributeValue("Speed", Speed.ToString());
        if(Speed3 is not null)
            e.SetAttributeValue("Speed3", Speed3.ToString());
        if(Speed4 is not null)
            e.SetAttributeValue("Speed4", Speed4.ToString());
        
        if(LoopIdx != 0)
            e.SetAttributeValue("LoopIdx", LoopIdx.ToString());
        
        foreach(CustomPath c in CustomPaths)
            e.Add(c.Serialize());
        foreach(Group g in Groups)
            e.Add(g.Serialize());

        return e;
    }
}