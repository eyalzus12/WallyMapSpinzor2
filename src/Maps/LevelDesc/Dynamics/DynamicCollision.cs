using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : AbstractDynamic<AbstractCollision>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeCollisionChildren();
    }

    public override void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        foreach (AbstractCollision c in Children)
            c.CalculateCurve(X, Y);
        base.DrawOn(canvas, trans, config, context, state);
    }
}