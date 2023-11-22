using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelTypes : IDeserializable, ISerializable
{
    public List<LevelType> Levels{get; set;} = null!;

    public void Deserialize(XElement e)
    {
        Levels = e.DeserializeChildrenOfType<LevelType>();
    }

    public void Serialize(XElement e)
    {
        foreach(LevelType lt in Levels)
            e.Add(lt.SerializeToXElement());
    }
}