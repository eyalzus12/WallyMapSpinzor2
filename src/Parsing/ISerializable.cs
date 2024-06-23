using System.Xml.Linq;

namespace WallyMapSpinzor2;

public interface ISerializable
{
    void Serialize(XElement e);
}