using System.Xml.Linq;

namespace WallyMapSpinzor2;

public interface IDeserializable
{
    void Deserialize(XElement element);
}