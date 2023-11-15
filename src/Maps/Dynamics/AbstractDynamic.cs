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
        foreach(T c in Children)
            e.Add(c.SerializeToXElement());
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time) 
        where TTexture : ITexture
    {
        if(rd.PlatIDDynamicOffset is null)
            throw new InvalidOperationException($"Plat ID dictionary was null when attempting to draw {GetType().Name}");
        if(!rd.PlatIDDynamicOffset.ContainsKey(PlatID))
            throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw {GetType().Name}. Make sure to call StoreOffset on all MovingPlatforms.");
        (double _X, double _Y) = rd.PlatIDDynamicOffset[PlatID];
        Transform tt = t * Transform.CreateTranslate(X + _X, Y + _Y);
        foreach(T c in Children)
            c.DrawOn(canvas, rd, rs, tt, time);
    }
}