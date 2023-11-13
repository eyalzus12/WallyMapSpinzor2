using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractKeyFrame : IDeserializable, ISerializable
{
    public abstract void Deserialize(XElement e);
    public abstract XElement Serialize();

    public abstract double GetStartFrame();
    public abstract (double, double) GetPosition();

    public abstract (double, double) LerpTo<T>
    (T kk, double? centerX, double? centerY, double time, double kTimeOffset = 0)
    where T : AbstractKeyFrame;
}