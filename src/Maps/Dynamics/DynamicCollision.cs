using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : AbstractDynamic<AbstractCollision>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeCollisionChildren();
    }
}