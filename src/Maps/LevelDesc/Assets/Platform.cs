using System;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Platform : AbstractAsset
{
    private const string NO_SKULLS = "am_NoSkulls";
    private const string HOTKEY = "am_Hotkey";
    private const string BLUE = "am_Blue";
    private const string RED = "am_Red";

    private const string SIMPLE_ASSET_SWAP = "simple";
    private const string ANIMTED_ASSET_SWAP = "animated";

    public string InstanceName { get; set; } = null!;
    public string? PlatformAssetSwap { get; set; }
    public string[]? Theme { get; set; }
    public string? ScoringType { get; set; }

    public AbstractAsset[]? AssetChildren { get; set; } = null!;

    public bool NoSkulls => InstanceName == NO_SKULLS;
    public string? Hotkey => InstanceName.StartsWith(HOTKEY) ? InstanceName[(InstanceName.LastIndexOf('_') + 1)..] : null;
    public int? Blue => InstanceName.StartsWith(BLUE) ? int.TryParse(InstanceName[BLUE.Length..], out int blue) ? blue : null : null;
    public int? Red => InstanceName.StartsWith(RED) ? int.TryParse(InstanceName[RED.Length..], out int red) ? red : null : null;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        InstanceName = e.GetAttribute("InstanceName");
        PlatformAssetSwap = e.GetAttributeOrNull("PlatformAssetSwap");
        Theme = e.GetAttributeOrNull("Theme")?.Split(',');
        ScoringType = e.GetAttributeOrNull("ScoringType");
        AssetChildren = AssetName is null ? e.DeserializeAssetChildren() : null;
        foreach (AbstractAsset a in AssetChildren ?? [])
            a.Parent = this;
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("InstanceName", InstanceName);

        if (PlatformAssetSwap is not null)
            e.SetAttributeValue("PlatformAssetSwap", PlatformAssetSwap);

        if (Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));

        if (ScoringType is not null)
            e.SetAttributeValue("ScoringType", ScoringType);

        base.Serialize(e);

        if (AssetName is null)
            e.AddManySerialized(AssetChildren!);
    }

    public override void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        //checks for showing assets. logic follows the game's code.
        if (!config.ShowAssets)
            return;

        if (NoSkulls)
        {
            if (!config.NoSkulls)
                return;
        }
        else if (Hotkey is not null)
        {
            if (Hotkey != Enum.GetName(config.Hotkey))
                return;
        }
        else if (Theme is not null || ScoringType is not null)
        {
            bool themeMatches = Theme?.Contains(Enum.GetName(config.Theme)!) ?? false;
            bool scoringTypeMatches = ScoringType is not null && (ScoringType == Enum.GetName(config.ScoringType));
            if (!themeMatches && !scoringTypeMatches)
                return;
        }
        else if (PlatformAssetSwap is not null)
        {
            if (PlatformAssetSwap == SIMPLE_ASSET_SWAP && config.AnimatedBackgrounds)
                return;
            if (PlatformAssetSwap == ANIMTED_ASSET_SWAP && !config.AnimatedBackgrounds)
                return;
        }
        if (Blue is not null && config.ShowPickedPlatform && Blue == config.PickedPlatform)
            return;
        else if (Red is not null && config.ShowPickedPlatform && Red != config.PickedPlatform)
            return;

        //normal asset
        if (AssetName is not null)
        {
            base.DrawOn(canvas, trans, config, context, state);
        }
        //not a normal asset
        else
        {
            Transform childTrans = trans * Transform;
            foreach (AbstractAsset a in AssetChildren!)
                a.DrawOn(canvas, childTrans, config, context, state);
        }
    }
}