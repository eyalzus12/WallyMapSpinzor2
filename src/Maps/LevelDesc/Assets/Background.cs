using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class Background : IDeserializable, ISerializable, IDrawable
{
    private const string BACKGROUND_FOLDER = "Backgrounds";

    //AssetName and AnimatedAssetName cannot both be non-null at the same time
    public string? AssetName { get; set; }
    public string? AnimatedAssetName { get; set; }
    public bool HasSkulls { get; set; }
    public List<string>? Theme { get; set; }
    public double H { get; set; }
    public double W { get; set; }

    public void Deserialize(XElement e)
    {
        AssetName = e.GetAttributeOrNull("AssetName");
        AnimatedAssetName = e.GetAttributeOrNull("AnimatedAssetName");
        HasSkulls = e.GetBoolAttribute("HasSkulls", false);
        Theme = e.GetAttributeOrNull("Theme")?.Split(',').ToList();
        H = e.GetFloatAttribute("H");
        W = e.GetFloatAttribute("W");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("AssetName", AssetName);
        if (AnimatedAssetName != null)
            e.SetAttributeValue("AnimatedAssetName", AnimatedAssetName);
        if (HasSkulls)
            e.SetAttributeValue("HasSkulls", HasSkulls.ToString());
        if (Theme is not null)
            e.SetAttributeValue("Theme", string.Join(',', Theme));
        e.SetAttributeValue("H", H.ToString());
        e.SetAttributeValue("W", W.ToString());
    }

    public void UpdateBackground(RenderData data, RenderConfig config)
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
            int themeCount1 = Theme?.Count ?? 0;
            int themeCount2 = data.CurrentBackground.Theme?.Count ?? 0;
            if (matchCount1 > matchCount2 || (matchCount1 == matchCount2 && themeCount1 < themeCount2))
                data.CurrentBackground = this;
        }
    }

    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        if (!config.ShowBackground) return;
        if (data.CurrentBackground != this) return;

        if (data.BackgroundRect_H is null || data.BackgroundRect_W is null || data.BackgroundRect_X is null || data.BackgroundRect_Y is null)
            throw new InvalidOperationException("Attempting to draw background, but global data is missing the background rect. Make sure the camera bounds are drawn before the background.");

        string assetName = ((config.AnimatedBackgrounds ? AnimatedAssetName : null) ?? AssetName)!;
        T texture = canvas.LoadTextureFromPath(Path.Join(BACKGROUND_FOLDER, assetName).ToString());
        canvas.DrawTextureRect(
            data.BackgroundRect_X ?? 0, data.BackgroundRect_Y ?? 0, data.BackgroundRect_W ?? 0, data.BackgroundRect_H ?? 0,
            texture, trans, DrawPriorityEnum.BACKGROUND
        );
    }
}