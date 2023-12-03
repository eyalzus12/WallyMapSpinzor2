using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractItemSpawn: IDeserializable, ISerializable, IDrawable
{
    public double H{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}

    public void Deserialize(XElement e)
    {
        H = e.GetFloatAttribute("H", DefaultH);
        W = e.GetFloatAttribute("W", DefaultW);
        X = e.GetFloatAttribute("X", DefaultX);
        Y = e.GetFloatAttribute("Y", DefaultY);
    }

    public void Serialize(XElement e)
    {
        if(H != DefaultH)
            e.SetAttributeValue("H", H.ToString());
        if(W != DefaultW)
            e.SetAttributeValue("W", W.ToString());

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
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
        canvas.DrawRect(X, Y, W, H, true, GetColor(rs), t, DrawPriorityEnum.DATA);
    }

    public abstract Color GetColor(RenderSettings rs);
}