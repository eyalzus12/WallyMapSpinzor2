using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomPath : IDeserializable, ISerializable
{
    public List<Point> Points{get; set;} = null!;
    public void Deserialize(XElement e)
    {
        Points = e.DeserializeChildrenOfType<Point>();
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(Points);
    }
}
