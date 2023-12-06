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


    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if(!config.ShowSpawnBotBounds) return;
        canvas.DrawRect(X, Y, W, H, false, config.ColorSpawnBotBounds, trans, DrawPriorityEnum.DATA);
    }
}