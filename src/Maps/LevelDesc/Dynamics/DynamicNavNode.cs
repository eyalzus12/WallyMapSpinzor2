using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicNavNode : AbstractDynamic<NavNode>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeChildrenOfType<NavNode>();
    }

    public void RegisterNavNodes(RenderData data)
    {
        (double dynX, double dynY) = data.PlatIDDynamicOffset[PlatID];
        foreach (NavNode n in Children)
            n.RegisterNavNode(data, X + dynX, Y + dynY);
    }
}