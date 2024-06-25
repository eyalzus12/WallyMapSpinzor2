using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Respawn : IDeserializable, ISerializable, IDrawable
{
    public bool Initial { get; set; }
    public bool ExpandedInit { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public DynamicRespawn? Parent { get; set; }

    public void Deserialize(XElement e)
    {
        Initial = e.GetBoolAttribute("Initial", false);
        ExpandedInit = e.GetBoolAttribute("ExpandedInit", false);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public void Serialize(XElement e)
    {
        if (Initial)
            e.SetAttributeValue("Initial", Initial);
        if (ExpandedInit)
            e.SetAttributeValue("ExpandedInit", ExpandedInit);
        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
    }


    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowRespawn) return;
        canvas.DrawCircle(
            X, Y,
            config.RadiusRespawn,
            GetColor(config),
            trans, DrawPriorityEnum.DATA,
            this
        );
    }

    public Color GetColor(RenderConfig config) =>
        Initial
            ? config.ColorInitialRespawn
            : ExpandedInit
                ? config.ColorExpandedInitRespawn
                : config.ColorRespawn;
}