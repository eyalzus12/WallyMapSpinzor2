using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Point : IDeserializable, ISerializable, IDrawable
{
    public double X { get; set; }
    public double Y { get; set; }

    public void Deserialize(XElement e)
    {
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
    }

    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        canvas.DrawCircle(X, Y, config.RadiusHordePathPoint, config.ColorHordePath, trans, DrawPriorityEnum.DATA);
    }
}