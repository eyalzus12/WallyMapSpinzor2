using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicRespawn : AbstractDynamic<Respawn>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeChildrenOfType<Respawn>();
        foreach (Respawn r in Children)
            r.Parent = this;
    }
}