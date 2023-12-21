using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractDynamic<T> : ISerializable, IDeserializable, IDrawable
    where T : ISerializable, IDeserializable, IDrawable
{
    public string PlatID{get; set;} = null!;
    public double X{get; set;}
    public double Y{get; set;}
    public List<T> Children{get; set;} = null!;

    public abstract void DeserializeChildren(XElement element);

    public void Deserialize(XElement e)
    {
        PlatID = e.GetAttribute("PlatID");
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
        DeserializeChildren(e);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.AddManySerialized(Children);
    }

    public virtual void DrawOn<E>(ICanvas<E> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data) 
        where E : ITexture
    {
        if(data.PlatIDDynamicOffset is null)
            throw new InvalidOperationException($"Plat ID dictionary was null when attempting to draw {GetType().Name}");
        if(!data.PlatIDDynamicOffset.ContainsKey(PlatID))
            throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw {GetType().Name}. Make sure to call StoreOffset on all MovingPlatforms.");
        (double dynX, double dynY) = data.PlatIDDynamicOffset[PlatID];
        Transform childTrans = trans * Transform.CreateTranslate(X + dynX, Y + dynY);
        foreach(T child in Children)
            child.DrawOn(canvas, config, childTrans, time, data);
    }
}
