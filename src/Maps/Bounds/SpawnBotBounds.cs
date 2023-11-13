using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class SpawnBotBounds : IDeserializable, ISerializable, IDrawable
{
    public double H{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement e)
    {
        H = e.GetFloatAttribute("H");
        W = e.GetFloatAttribute("W");
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
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
        if(!rs.ShowSpawnBotBounds) return;
        canvas.DrawRect(X, Y, W, H, false, rs.ColorSpawnBotBounds, t, DrawPriorityEnum.DATA);
    }
}