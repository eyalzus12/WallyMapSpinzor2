using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicItemSpawn : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public List<AbstractItemSpawn> ItemSpawns{get; set;} = null!;

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        PlatID = element.GetAttribute("PlatID");
        ItemSpawns = element.DeserializeItemSpawnChildren();
    }
}