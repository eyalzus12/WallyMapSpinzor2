using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Point : IDeserializable, ISerializable, IDrawable
{
    public double X { get; set; }
    public double Y { get; set; }

    public void Deserialize(XElement e)
    {
        X = e.GetDoubleAttribute("X");
        Y = e.GetDoubleAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        canvas.DrawCircle(X, Y, config.RadiusHordePathPoint, config.ColorHordePath, trans, DrawPriorityEnum.DATA, this);
    }
}