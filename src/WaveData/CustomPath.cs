using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomPath : IDeserializable
{
    public List<Point> Points{get; set;} = new();
    public void Deserialize(XElement element)
    {
        Points = element.DeserializeChildrenOfType<Point>();
    }
}