using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicNavNode : AbstractDynamic<NavNode>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeChildrenOfType<NavNode>();
    }

    public void RegisterNavNodes(RenderContext context)
    {
        (double dynX, double dynY) = context.PlatIDDynamicOffset[PlatID];
        foreach (NavNode n in Children)
            n.RegisterNavNode(context, X + dynX, Y + dynY);
    }
}