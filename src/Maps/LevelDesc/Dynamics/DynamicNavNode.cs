using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicNavNode : AbstractDynamic<NavNode>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeChildrenOfType<NavNode>();
        foreach (NavNode n in Children)
            n.Parent = this;
    }

    public void RegisterNavNodes(RenderContext context)
    {
        (double dynX, double dynY) = GetOffset(context);
        foreach (NavNode n in Children)
            n.RegisterNavNode(context, X + dynX, Y + dynY);
    }
}