using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class NavNode : IDeserializable, ISerializable, IDrawable
{
    public string NavID{get; set;} = null!;
    public List<string> Path{get; set;} = null!;
    public double X{get; set;}
    public double Y{get; set;}

    public void Deserialize(XElement e)
    {
        NavID = e.GetAttribute("NavID");
        Path = e.GetAttribute("Path").Split(',').ToList();
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("NavID", NavID);
        e.SetAttributeValue("Path", string.Join(',', Path));
        if(X != 0)
            e.SetAttributeValue("X", X.ToString());
        if(Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, TimeSpan time) 
        where TTexture : ITexture
    {
        if(!rs.ShowNavNode) return;
        canvas.DrawCircle(X, Y, 10, Color.FromHex(0x0000007F), t, DrawPriorityEnum.NAVNODE);
    }
}