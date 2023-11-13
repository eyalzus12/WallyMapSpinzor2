using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Respawn : IDeserializable, ISerializable, IDrawable
{
    public bool Initial{get; set;}
    public bool ExpandedInit{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement element)
    {
        Initial = element.GetBoolAttribute("Initial", false);
        ExpandedInit = element.GetBoolAttribute("ExpandedInit", false);
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
    }

    public XElement Serialize()
    {
        XElement e = new("Respawn");

        if(Initial)
            e.SetAttributeValue("Initial", Initial);
        if(ExpandedInit)
            e.SetAttributeValue("ExpandedInit", ExpandedInit);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        return e;
    }

    
    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!rs.ShowRespawn) return;
        canvas.DrawCircle(
            X, Y,
            rs.RadiusRespawn,
            Initial?rs.ColorInitialRespawn:ExpandedInit?rs.ColorExpandedInitRespawn:rs.ColorRespawn,
            t, DrawPriorityEnum.DATA
        );
    }
}