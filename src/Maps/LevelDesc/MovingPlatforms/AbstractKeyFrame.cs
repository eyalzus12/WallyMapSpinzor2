using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractKeyFrame : IDeserializable, ISerializable
{
    public abstract void Deserialize(XElement e);
    public abstract void Serialize(XElement e);

    public abstract (double, double) GetPosition();

    public abstract void GetImplicitKeyFrames(List<KeyFrame> output, int index, int startFrame);
}