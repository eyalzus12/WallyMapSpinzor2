using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : AbstractDynamic<AbstractCollision>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeCollisionChildren();
    }

    public override void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
    {
        foreach (AbstractCollision c in Children)
            c.CalculateCurve(X, Y);
        base.DrawOn(canvas, config, trans, time, data);
    }
}