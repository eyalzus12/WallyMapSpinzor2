using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Background : IDeserializable, ISerializable, IDrawable
{
    //AssetName and AnimatedAssetName cannot both be non-null at the same time
    public string? AssetName{get; set;}
    public string? AnimatedAssetName{get; set;}
    public bool HasSkulls{get; set;}
    public List<string>? Theme{get; set;}
    public double H{get; set;}
    public double W{get; set;}

    public void Deserialize(XElement element)
    {
        AssetName = element.GetNullableAttribute("AssetName");
        AnimatedAssetName = element.GetNullableAttribute("AnimatedAssetName");
        HasSkulls = element.GetBoolAttribute("HasSkulls", false);
        Theme = element.GetNullableAttribute("Theme")?.Split(',').ToList();
        H = element.GetFloatAttribute("H");
        W = element.GetFloatAttribute("W");
    }

    public XElement Serialize()
    {
        XElement e = new("Background");

        e.SetAttributeValue("AssetName", AssetName);
        if(AnimatedAssetName != null)
            e.SetAttributeValue("AnimatedAssetName", AnimatedAssetName);
        if(HasSkulls)
            e.SetAttributeValue("HasSkulls", HasSkulls.ToString());
        if(Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("W", W.ToString());

        return e;
    }

    public void ChallengeCurrentBackground(GlobalRenderData rd, RenderSettings rs)
    {
        if(rd.CurrentBackground is null)
        {
            rd.CurrentBackground = this;
        }
        else if(HasSkulls != rd.CurrentBackground.HasSkulls)
        {
            if(rs.NoSkulls == rd.CurrentBackground.HasSkulls)
                rd.CurrentBackground = this;
        }
        else
        {
            int matchCount1 = Theme?.Count(t => t == rs.Theme) ?? 0;
            int matchCount2 = rd.CurrentBackground.Theme?.Count(t => t == rs.Theme) ?? 0;
            int themeCount1 = Theme?.Count ?? 0;
            int themeCount2 = rd.CurrentBackground.Theme?.Count ?? 0;
            if(matchCount1 > matchCount2 || (matchCount1 == matchCount2 && themeCount1 < themeCount2))
                rd.CurrentBackground = this;
        }
    }

    public void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time) 
        where TTexture : ITexture
    {
        if(!rs.ShowBackground) return;
        if(rd.CurrentBackground != this) return;

        if(rd.BackgroundRect_H is null || rd.BackgroundRect_W is null || rd.BackgroundRect_X is null || rd.BackgroundRect_Y is null)
            throw new InvalidOperationException("Attempting to draw background, but global data is missing the background rect. Make sure the camera bounds are drawn before the background.");
        
        string assetName = ((rs.AnimatedBackgrounds ? AnimatedAssetName : null) ?? AssetName)!;
        string path = Path.Join("Backgrounds", assetName).ToString();
        TTexture texture = canvas.LoadTextureFromPath(path);
        canvas.DrawTextureRect(
            rd.BackgroundRect_X??0, rd.BackgroundRect_Y??0, rd.BackgroundRect_W??0, rd.BackgroundRect_H??0,
            texture, t, DrawPriorityEnum.BACKGROUND
        );
    }
}