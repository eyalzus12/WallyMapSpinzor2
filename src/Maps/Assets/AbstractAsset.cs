using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractAsset : ISerializable, IDeserializable, IDrawable
{
    public double X{get; set;}
    public double Y{get; set;}
    public double W{get; set;}
    public double H{get; set;}
    public double ScaleX{get; set;}
    public double ScaleY{get; set;}
    public double Rotation{get; set;}
    public string? AssetName{get; set;}

    public virtual void Deserialize(XElement element)
    {
        X = element.GetFloatAttribute("X", 0);
        Y = element.GetFloatAttribute("Y", 0);
        W = element.GetFloatAttribute("W", 0);
        H = element.GetFloatAttribute("H", 0);
        double Scale = element.GetFloatAttribute("Scale", 1);
        ScaleX = element.GetFloatAttribute("ScaleX", Scale);
        ScaleY = element.GetFloatAttribute("ScaleY", Scale);
        Rotation = element.GetFloatAttribute("Rotation", 0);
        AssetName = element.GetNullableAttribute("AssetName");
    }

    public virtual XElement Serialize()
    {
        XElement e = new(this.GetType().Name);

        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
        if(W != 0)
            e.SetAttributeValue("W", W.ToString());
        if(H != 0)
            e.SetAttributeValue("H", H.ToString());

        if(ScaleX == ScaleY)
        {
            e.SetAttributeValue("Scale", ScaleX.ToString());
        }
        else
        {
            e.SetAttributeValue("ScaleX", ScaleX.ToString());
            e.SetAttributeValue("ScaleY", ScaleY.ToString());
        }

        if(Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation.ToString());

        return e;
    }

    
    public virtual void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture
    {
        if(!rs.ShowAssets)
            return;

        if(AssetName is not null)
        {
            if(rd.AssetDir is null)
                throw new InvalidOperationException("Attempting to draw an asset, but global data is missing the AssetDir.");
            string path = Path.Join(rd.AssetDir, AssetName).ToString();
            TTexture texture = canvas.LoadTextureFromPath(path);
            double _W = (W != 0)?Math.Abs(W):texture.W;
            double _H = (H != 0)?Math.Abs(H):texture.H;
            canvas.DrawTextureRect(0, 0, _W, _H, texture, t * Transform, DrawPriorityEnum.MIDGROUND);
        }
    }

    public Transform Transform =>
        Transform.CreateFrom(
            X, Y, 
            Rotation * Math.PI / 180,
            0, 0,
            ScaleX * ((W < 0)?-1:1), ScaleY * ((H < 0)?-1:1)
        );
}