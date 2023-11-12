using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractItemSpawn: IDeserializable, ISerializable, IDrawable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}

    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", DefaultX);
        Y = element.GetFloatAttribute("Y", DefaultY);
        W = element.GetFloatAttribute("W", DefaultW);
        H = element.GetFloatAttribute("H", DefaultH);
    }

    public XElement Serialize()
    {
        XElement e = new(GetType().Name);

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        if(W != DefaultW)
            e.SetAttributeValue("W", W.ToString());
        if(H != DefaultH)
            e.SetAttributeValue("H", H.ToString());

        return e;
    }

    public abstract double DefaultX{get;}
    public abstract double DefaultY{get;}
    public abstract double DefaultW{get;}
    public abstract double DefaultH{get;}

    public virtual void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!rs.ShowItemSpawn) return;
        canvas.DrawRect(X, Y, W, H, true, Color(rs), t, DrawPriorityEnum.DATA);
    }

    public abstract Color Color(RenderSettings rs);
}