using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Respawn : IDeserializable, ISerializable, IDrawable
{
    public bool Initial{get; set;}
    public bool ExpandedInit{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement e)
    {
        Initial = e.GetBoolAttribute("Initial", false);
        ExpandedInit = e.GetBoolAttribute("ExpandedInit", false);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public void Serialize(XElement e)
    {
        if(Initial)
            e.SetAttributeValue("Initial", Initial);
        if(ExpandedInit)
            e.SetAttributeValue("ExpandedInit", ExpandedInit);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
    }

    
    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if(!config.ShowRespawn) return;
        canvas.DrawCircle(
            X, Y,
            config.RadiusRespawn,
            Initial ? config.ColorInitialRespawn : (ExpandedInit ? config.ColorExpandedInitRespawn : config.ColorRespawn),
            trans, DrawPriorityEnum.DATA
        );
    }
}
