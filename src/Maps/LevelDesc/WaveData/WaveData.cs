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

    public void Deserialize(XElement e)
    {
        ID = e.GetIntAttribute("ID", 0);
        Speed = e.GetNullableFloatAttribute("Speed");
        Speed3 = e.GetNullableFloatAttribute("Speed3");
        Speed4 = e.GetNullableFloatAttribute("Speed4");
        LoopIdx = e.GetIntAttribute("LoopIdx", 0);
        CustomPaths = e.DeserializeChildrenOfType<CustomPath>();
        Groups = e.DeserializeChildrenOfType<Group>();
    }

    public void Serialize(XElement e)
    {
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
            e.Add(c.SerializeToXElement());
        foreach(Group g in Groups)
            e.Add(g.SerializeToXElement());
    }
}