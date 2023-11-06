using System.Xml.Linq;

namespace WallyMapSpinzor2;

public interface ISerializable
{
    XElement Serialize();
}