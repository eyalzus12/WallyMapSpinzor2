using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractItemSpawn : IDeserializable, ISerializable, IDrawable
{
    public double H { get; set; }
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public void Deserialize(XElement e)
    {
        H = e.GetFloatAttribute("H", DefaultH);
        W = e.GetFloatAttribute("W", DefaultW);
        X = e.GetFloatAttribute("X", DefaultX);
        Y = e.GetFloatAttribute("Y", DefaultY);
    }

    public void Serialize(XElement e)
    {
        if (H != DefaultH)
            e.SetAttributeValue("H", H);
        if (W != DefaultW)
            e.SetAttributeValue("W", W);

        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
    }

    public abstract double DefaultX { get; }
    public abstract double DefaultY { get; }
    public abstract double DefaultW { get; }
    public abstract double DefaultH { get; }

    public virtual void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowItemSpawn) return;
        canvas.DrawRect(X, Y, W, H, true, GetColor(config), trans, DrawPriorityEnum.DATA, this);
    }

    public abstract Color GetColor(RenderConfig config);
}