using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class DynamicCollision : AbstractDynamic<AbstractCollision>
{
    public override void DeserializeChildren(XElement element)
    {
        Children = element.DeserializeCollisionChildren();
    }
    
    public override void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, TimeSpan time)
    {
        foreach(AbstractCollision c in Children)
            c.CalculateCurve(X, Y);
        base.DrawOn(canvas, rd, rs, t, time);
    }
}