using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSetTypes : IDeserializable, ISerializable
{
    public List<LevelSetType> Playlists{get; set;} = null!;

    public void Deserialize(XElement e)
    {
        Playlists = e.DeserializeChildrenOfType<LevelSetType>();
    }

    public void Serialize(XElement e)
    {
        foreach(LevelSetType lst in Playlists)
            e.Add(lst.SerializeToXElement());
    }
}