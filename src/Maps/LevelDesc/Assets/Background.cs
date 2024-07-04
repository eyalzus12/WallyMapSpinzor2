using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Background : IDeserializable, ISerializable, IDrawable
{
    private const string BACKGROUND_FOLDER = "Backgrounds";

    public string AssetName { get; set; } = null!;
    public string? AnimatedAssetName { get; set; }
    public bool HasSkulls { get; set; }
    public string[]? Theme { get; set; }
    public double H { get; set; }
    public double W { get; set; }

    public void Deserialize(XElement e)
    {
        AssetName = e.GetAttribute("AssetName");
        AnimatedAssetName = e.GetAttributeOrNull("AnimatedAssetName");
        HasSkulls = e.GetBoolAttribute("HasSkulls", false);
        Theme = e.GetAttributeOrNull("Theme")?.Split(',');
        H = e.GetDoubleAttribute("H");
        W = e.GetDoubleAttribute("W");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("AssetName", AssetName);
        if (AnimatedAssetName != null)
            e.SetAttributeValue("AnimatedAssetName", AnimatedAssetName);
        if (HasSkulls)
            e.SetAttributeValue("HasSkulls", HasSkulls);
        if (Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));
        e.SetAttributeValue("H", H);
        e.SetAttributeValue("W", W);
    }

    public void UpdateBackground(RenderContext data, RenderConfig config)
    {
        if (data.CurrentBackground is null)
        {
            data.CurrentBackground = this;
        }
        else if (HasSkulls != data.CurrentBackground.HasSkulls)
        {
            if (config.NoSkulls == data.CurrentBackground.HasSkulls)
                data.CurrentBackground = this;
        }
        else
        {
            int matchCount1 = Theme?.Count(t => t == Enum.GetName(config.Theme)) ?? 0;
            int matchCount2 = data.CurrentBackground.Theme?.Count(t => t == Enum.GetName(config.Theme)) ?? 0;
            int themeCount1 = Theme?.Length ?? 0;
            int themeCount2 = data.CurrentBackground.Theme?.Length ?? 0;
            if (matchCount1 > matchCount2 || (matchCount1 == matchCount2 && themeCount1 < themeCount2))
                data.CurrentBackground = this;
        }
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowBackground) return;
        if (context.CurrentBackground != this) return;

        if (context.BackgroundRect_H is null || context.BackgroundRect_W is null || context.BackgroundRect_X is null || context.BackgroundRect_Y is null)
            throw new InvalidOperationException("Attempting to draw background, but render context is missing the background rect. Make sure the camera bounds are drawn before the background.");

        string assetName = (config.AnimatedBackgrounds ? AnimatedAssetName : null) ?? AssetName;
        canvas.DrawTextureRect(
            Path.Combine(BACKGROUND_FOLDER, assetName),
            context.BackgroundRect_X ?? 0, context.BackgroundRect_Y ?? 0, context.BackgroundRect_W ?? 0, context.BackgroundRect_H ?? 0,
            trans, DrawPriorityEnum.BACKGROUND,
            this
        );
    }
}