using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicNavNode : AbstractDynamic<NavNode>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeChildrenOfType<NavNode>();
    }
}