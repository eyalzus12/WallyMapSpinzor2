using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class WaveData : IDeserializable
{
    public int ID{get; set;}
    public double? Speed{get; set;}
    public double? Speed3{get; set;}
    public double? Speed4{get; set;}
    public int LoopIdx{get; set;}
    public List<CustomPath> CustomPaths{get; set;} = new();
    public List<Group> Groups{get; set;} = new();

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
}