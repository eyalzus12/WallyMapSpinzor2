using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnim : IDeserializable, ISerializable, IDrawable
{
    private const string FOREGROUND = "am_Foreground";
    private const string BACKGROUND = "am_Background";
    private const string PLATFORM = "am_Platform";
    private const string BRAWL_GOAL = "am_BrawlGoal";
    private const string COLOR_PLATFORM = "am_ColorPlatform";

    public string InstanceName { get; set; } = null!;
    public string AssetName { get; set; } = null!;
    public double X { get; set; }
    public double Y { get; set; }

    //never actually used.
    public bool Foreground => InstanceName.StartsWith(FOREGROUND);
    //never actually used.
    public bool Background => InstanceName.StartsWith(BACKGROUND);

    //the next 3 are all added to some list. unclear what the list is relevant for.

    //indicates moving platform. format: am_Platform_[PlatID]
    public bool Platform => InstanceName.StartsWith(PLATFORM);
    //indicates brawlball goal. if InstanceName contains "1", it is goal 1. otherwise goal 2.
    public bool BrawlGoal => InstanceName.StartsWith(BRAWL_GOAL);
    //supposedly related to platform king, but code doesn't reveal much.
    public bool ColorPlatform => InstanceName.StartsWith(COLOR_PLATFORM);


    public void Deserialize(XElement e)
    {
        InstanceName = e.GetAttribute("InstanceName");
        AssetName = e.GetAttribute("AssetName");
        X = e.GetDoubleAttribute("X");
        Y = e.GetDoubleAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("InstanceName", InstanceName);
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("X", X);
        e.SetAttributeValue("Y", Y);
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowAssets) return;
        Gfx gfx = new()
        {
            AnimFile = "SFX_Level.swf",
            AnimClass = AssetName,
            FireAndForget = false,
            RandomFrameStart = true,
            Desynch = true,
        };
        canvas.DrawAnim(gfx, "", 0, trans * Transform.CreateTranslate(X, Y), DrawPriorityEnum.MIDGROUND, this);
    }
}