using System;
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

    public virtual void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (context.PlatIDDynamicOffset is null)
            throw new InvalidOperationException($"Plat ID dictionary was null when attempting to draw {GetType().Name}");
        if (!context.PlatIDDynamicOffset.TryGetValue(PlatID, out (double, double) dynOffset))
            throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw {GetType().Name}. Make sure to call StoreOffset on all MovingPlatforms.");
        (double dynX, double dynY) = dynOffset;
        Transform childTrans = trans * Transform.CreateTranslate(X + dynX, Y + dynY);
        foreach (T child in Children)
            child.DrawOn(canvas, childTrans, config, context, state);
    }
}