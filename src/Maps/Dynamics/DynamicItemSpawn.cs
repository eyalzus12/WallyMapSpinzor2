using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicItemSpawn : IDeserializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public List<AbstractItemSpawn> ItemSpawns{get; set;} = null!;

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        PlatID = element.GetAttribute("PlatID");
        ItemSpawns = element.DeserializeItemSpawnChildren();
    }

    public XElement Serialize()
    {
        XElement e = new("DynamicItemSpawn");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);

        foreach(AbstractItemSpawn i in ItemSpawns)
            e.Add(i.Serialize());

        return e;
    }
}