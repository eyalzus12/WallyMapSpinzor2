using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicRespawn : IDeserializable, ISerializable
{
    public double X{get; set;}
    public double Y{get; set;}
    public string PlatID{get; set;} = null!;
    public List<Respawn> Respawns{get; set;} = null!;

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        PlatID = element.GetAttribute("PlatID");
        Respawns = element.DeserializeChildrenOfType<Respawn>();
    }

    public XElement Serialize()
    {
        XElement e = new("DynamicRespawn");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("PlatID", PlatID);

        foreach(Respawn r in Respawns)
            e.Add(r.Serialize());

        return e;
    }
}