using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CameraBounds : IDeserializable, ISerializable, IDrawable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}
    public void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        W = element.GetFloatAttribute("W", 0);
        H = element.GetFloatAttribute("H", 0);
    }

    public XElement Serialize()
    {
        XElement e = new("CameraBounds");

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("H", H.ToString());

        return e;
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        rd.BackgroundRect_X = X;
        rd.BackgroundRect_Y = Y;
        rd.BackgroundRect_W = W;
        rd.BackgroundRect_H = H;

        if(!rs.ShowCameraBounds) return;
        canvas.DrawRect(X, Y, W, H, false, rs.ColorCameraBounds, t, DrawPriorityEnum.DATA);
    }
}