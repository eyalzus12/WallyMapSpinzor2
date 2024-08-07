using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractItemSpawn : IDeserializable, ISerializable, IDrawable
{
    public double H { get; set; }
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public DynamicItemSpawn? Parent { get; set; }

    public void Deserialize(XElement e)
    {
        H = e.GetDoubleAttribute("H", DefaultH);
        W = e.GetDoubleAttribute("W", DefaultW);
        X = e.GetDoubleAttribute("X", DefaultX);
        Y = e.GetDoubleAttribute("Y", DefaultY);
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

    public virtual void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowItemSpawn) return;
        canvas.DrawRect(X, Y, W, H, true, GetColor(config), trans, DrawPriorityEnum.DATA, this);
    }

    public abstract Color GetColor(RenderConfig config);
}