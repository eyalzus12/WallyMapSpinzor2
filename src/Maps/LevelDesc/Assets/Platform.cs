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
    public string? Hotkey => InstanceName.StartsWith("am_Hotkey")?InstanceName[(InstanceName.LastIndexOf('_') + 1)..] :null;
    public int? Blue => InstanceName.StartsWith("am_Blue")?int.TryParse(InstanceName["am_Blue".Length..], out int blue)?blue:null:null;
    public int? Red => InstanceName.StartsWith("am_Red")?int.TryParse(InstanceName["am_Red".Length..], out int red)?red:null:null;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        InstanceName = e.GetAttribute("InstanceName");
        PlatformAssetSwap = e.GetNullableAttribute("PlatformAssetSwap");
        Theme = e.GetNullableAttribute("Theme")?.Split(',').ToList();
        ScoringType = e.GetNullableAttribute("ScoringType");
        AssetChildren = e.DeserializeAssetChildren();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("InstanceName", InstanceName);
        
        if(PlatformAssetSwap is not null)
            e.SetAttributeValue("PlatformAssetSwap", PlatformAssetSwap);
        
        if(Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));
        
        if(ScoringType is not null)
            e.SetAttributeValue("ScoringType", ScoringType);
        
        base.Serialize(e);

        foreach(AbstractAsset a in AssetChildren)
            e.Add(a.SerializeToXElement());
    }

    
    public override void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, TimeSpan time)
    {
        //checks for showing assets. logic follows the game's code.
        if(!rs.ShowAssets)
            return;
        
        if(NoSkulls)
        {
            if(!rs.NoSkulls)
                return;
        }
        else if(Hotkey is not null)
        {
            if(Hotkey != rs.Hotkey)
                return;
        }
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

        //normal asset
        if(AssetName is not null)
        {
            base.DrawOn(canvas, rd, rs, t, time);
        }
        //not a normal asset
        else
        {
            Transform tt = t * Transform;
            foreach(AbstractAsset a in AssetChildren)
                a.DrawOn(canvas, rd, rs, tt, time);
        }
    }
}