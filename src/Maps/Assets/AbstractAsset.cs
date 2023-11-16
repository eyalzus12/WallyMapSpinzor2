using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractAsset : ISerializable, IDeserializable, IDrawable
{
    public string? AssetName{get; set;}
    public double Rotation{get; set;}
    public double ScaleX{get; set;}
    public double ScaleY{get; set;}
    public double H{get; set;}
    public double W{get; set;}
    public double X{get; set;}
    public double Y{get; set;}

    public virtual void Deserialize(XElement e)
    {
        AssetName = e.GetNullableAttribute("AssetName");
        Rotation = e.GetFloatAttribute("Rotation", 0);
        double Scale = e.GetFloatAttribute("Scale", 1);
        ScaleX = e.GetFloatAttribute("ScaleX", Scale);
        ScaleY = e.GetFloatAttribute("ScaleY", Scale);
        H = e.GetFloatAttribute("H", 0);
        W = e.GetFloatAttribute("W", 0);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public virtual void Serialize(XElement e)
    {
        if(AssetName is not null)
            e.SetAttributeValue("AssetName", AssetName);

        if(Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());

        if(ScaleX == ScaleY)
        {
            if(ScaleX != 1)
                e.SetAttributeValue("Scale", ScaleX.ToString());
        }
        else
        {
            if(ScaleX != 1)
                e.SetAttributeValue("ScaleX", ScaleX.ToString());
            if(ScaleY != 1)
                e.SetAttributeValue("ScaleY", ScaleY.ToString());
        }
        
        if(H != 0)
            e.SetAttributeValue("H", H.ToString());
        if(W != 0)
            e.SetAttributeValue("W", W.ToString());
        if(X != 0)
            e.SetAttributeValue("X", X.ToString());
        if(Y != 0)
            e.SetAttributeValue("Y", Y.ToString());
    }

    
    public virtual void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!rs.ShowAssets)
            return;

        if(AssetName is null) return;

        if(rd.AssetDir is null)
            throw new InvalidOperationException("Attempting to draw an asset, but global data is missing the AssetDir.");
        string path = Path.Join(rd.AssetDir, AssetName).ToString();
        TTexture texture = canvas.LoadTextureFromPath(path);
        double _W = (W != 0)?Math.Abs(W):texture.W;
        double _H = (H != 0)?Math.Abs(H):texture.H;
        canvas.DrawTextureRect(0, 0, _W, _H, texture, t * Transform, DrawPriorityEnum.MIDGROUND);
    }

    public Transform Transform =>
        Transform.CreateScale((W < 0)?-1:1, (H < 0)?-1:1) *
        Transform.CreateFrom(
            x : X,
            y : Y, 
            rot : Rotation * Math.PI / 180,
            scaleX : ScaleX,
            scaleY : ScaleY
        );
}