using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Platform : AbstractAsset
{
    public string InstanceName{get; set;} = null!;
    public string? PlatformAssetSwap{get; set;}
    public List<string>? Theme{get; set;}
    public string? ScoringType{get; set;}

    public List<AbstractAsset> AssetChildren{get; set;} = null!;
    
    public bool NoSkulls => InstanceName == "am_NoSkulls";
    public string? Hotkey => InstanceName.StartsWith("am_Hotkey")?InstanceName.Substring(InstanceName.LastIndexOf('_')+1):null;
    public int? Blue => InstanceName.StartsWith("am_Blue")?int.Parse(InstanceName.Substring("am_Blue".Length)):null;
    public int? Red => InstanceName.StartsWith("am_Red")?int.Parse(InstanceName.Substring("am_Red".Length)):null;

    public override void Deserialize(XElement element)
    {
        base.Deserialize(element);
        InstanceName = element.GetAttribute("InstanceName");
        PlatformAssetSwap = element.GetNullableAttribute("PlatformAssetSwap");
        Theme = element.GetNullableAttribute("Theme")?.Split(',').ToList();
        ScoringType = element.GetNullableAttribute("ScoringType");
        AssetChildren = element.DeserializeAssetChildren();
    }

    public override XElement Serialize()
    {
        XElement e = new("Platform");

        e.SetAttributeValue("InstanceName", InstanceName);
        
        if(PlatformAssetSwap is not null)
            e.SetAttributeValue("PlatformAssetSwap", PlatformAssetSwap);
        
        if(Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));
        
        if(ScoringType is not null)
            e.SetAttributeValue("ScoringType", ScoringType);
        
        //hack to get InstanceName to show before the other stuff
        XElement e2 = base.Serialize();
        foreach(XAttribute attr in e2.Attributes())
            e.SetAttributeValue(attr.Name, attr.Value);

        foreach(AbstractAsset a in AssetChildren)
            e.Add(a.Serialize());

        return e;
    }

    
    public override void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
    {
        //checks for showing assets. logic follows the game's code.
        if(!rs.ShowAssets)
            return;
        else if(NoSkulls)
            if(!rs.NoSkulls) return;
        else if(Hotkey is not null)
            if(Hotkey != rs.Hotkey) return;
        else if(Theme is not null || ScoringType is not null)
        {
            bool themeMatches = Theme?.Contains(rs.Theme) ?? false;
            bool scoringTypeMatches = ScoringType is not null && (ScoringType == rs.ScoringType);
            if(!themeMatches && !scoringTypeMatches)
                return;
        }
        else if(PlatformAssetSwap is not null)
        {
            if(PlatformAssetSwap == "simple" && rs.AnimatedBackgrounds)
                return;
            if(PlatformAssetSwap == "animated" && !rs.AnimatedBackgrounds)
                return;
        }
        if(Blue is not null && Blue == rs.PickedPlatform)
            return;
        else if(Red is not null && Red != rs.PickedPlatform)
            return;

        //will only draw if AssetName is not null
        base.DrawOn(canvas, rd, rs, t, time);

        if(AssetName is null)
        {
            foreach(AbstractAsset a in AssetChildren)
                a.DrawOn(canvas, rd, rs, t * Transform, time);
        }
    }
}