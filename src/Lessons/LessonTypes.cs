using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonTypes : IDeserializable, ISerializable
{
    public LessonType[] Lessons { get; set; } = null!;

    public void Deserialize(XElement e)
    {
        Lessons = e.DeserializeChildrenOfType<LessonType>();
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(Lessons);
    }
}