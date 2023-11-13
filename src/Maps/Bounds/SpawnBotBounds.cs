using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class SpawnBotBounds : IDeserializable, ISerializable, IDrawable
{
    public double H{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}
    public void Deserialize(XElement element)
    {
        H = element.GetFloatAttribute("H");
        W = element.GetFloatAttribute("W");
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
    }

    public XElement Serialize()
    {
        XElement e = new("SpawnBotBounds");

        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("W", W.ToString());
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        return e;
    }


    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!rs.ShowSpawnBotBounds) return;
        canvas.DrawRect(X, Y, W, H, false, rs.ColorSpawnBotBounds, t, DrawPriorityEnum.DATA);
    }
}