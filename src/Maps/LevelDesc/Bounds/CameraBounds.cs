using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CameraBounds : IDeserializable, ISerializable, IDrawable
{
    public double H{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement e)
    {
        H = e.GetFloatAttribute("H", 0);
        W = e.GetFloatAttribute("W", 0);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
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