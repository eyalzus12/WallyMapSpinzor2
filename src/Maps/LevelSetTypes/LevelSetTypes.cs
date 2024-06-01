using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelSetTypes : IDeserializable, ISerializable
{
    public LevelSetType[] Playlists { get; set; } = null!;

    public void Deserialize(XElement e)
    {
        Playlists = e.DeserializeChildrenOfType<LevelSetType>();
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(Playlists);
    }
}