using System.Collections.Generic;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractKeyFrame : IDeserializable, ISerializable
{
    public Phase? Parent { get; set; }

    public abstract void Deserialize(XElement e);
    public abstract void Serialize(XElement e);

    public abstract void GetImplicitKeyFrames(List<KeyFrame> output, int index, int startFrame);
}