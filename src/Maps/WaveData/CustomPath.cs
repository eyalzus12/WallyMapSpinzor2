using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomPath : IDeserializable, ISerializable
{
    public List<Point> Points{get; set;} = null!;
    public void Deserialize(XElement element)
    {
        Points = element.DeserializeChildrenOfType<Point>();
    }

    public XElement Serialize()
    {
        XElement e = new("CustomPath");
        
        foreach(Point p in Points)
            e.Add(p.Serialize());

        return e;
    }
}