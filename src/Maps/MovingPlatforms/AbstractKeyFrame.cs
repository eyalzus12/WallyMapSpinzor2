using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractKeyFrame : IDeserializable, ISerializable
{
    public abstract void Deserialize(XElement e);
    public abstract void Serialize(XElement e);

    public abstract double GetStartFrame();
    public abstract (double, double) GetPosition();

    public abstract (double, double) LerpTo<T>
    (T kk, Animation.AnimationDefaultValues defaults, double numframes, double time, double fromTimeOffset, double toTimeOffset)
    where T : AbstractKeyFrame;
}