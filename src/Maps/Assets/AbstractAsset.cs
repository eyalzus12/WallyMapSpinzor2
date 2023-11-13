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

    public virtual void Deserialize(XElement element)
    {
        AssetName = element.GetNullableAttribute("AssetName");
        Rotation = element.GetFloatAttribute("Rotation", 0);
        double Scale = element.GetFloatAttribute("Scale", 1);
        ScaleX = element.GetFloatAttribute("ScaleX", Scale);
        ScaleY = element.GetFloatAttribute("ScaleY", Scale);
        H = element.GetFloatAttribute("H", 0);
        W = element.GetFloatAttribute("W", 0);
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
    }

    public virtual XElement Serialize()
    {
        XElement e = new(this.GetType().Name);
        
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

        return e;
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
        Transform.CreateFrom(
            X, Y, 
            Rotation * Math.PI / 180,
            0, 0,
            ScaleX * ((W < 0)?-1:1), ScaleY * ((H < 0)?-1:1)
        );
}