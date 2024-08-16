using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractDynamic<T> : ISerializable, IDeserializable, IDrawable
    where T : ISerializable, IDeserializable, IDrawable
{
    public string PlatID { get; set; } = null!;
    public double X { get; set; }
    public double Y { get; set; }
    public T[] Children { get; set; } = null!;

    public abstract void DeserializeChildren(XElement element);

    public void Deserialize(XElement e)
    {
        PlatID = e.GetAttribute("PlatID");
        X = e.GetDoubleAttribute("X");
        Y = e.GetDoubleAttribute("Y");
        DeserializeChildren(e);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
        e.AddManySerialized(Children);
    }

    public virtual (double, double) GetOffset(RenderContext context) =>
        context.PlatIDDynamicOffset.TryGetValue(PlatID, out (double, double) dynOffset)
            ? dynOffset
            : (0, 0);

    public virtual void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        (double dynX, double dynY) = GetOffset(context);
        Transform childTrans = trans * Transform.CreateTranslate(X + dynX, Y + dynY);
        foreach (T child in Children)
            child.DrawOn(canvas, childTrans, config, context, state);
    }
}