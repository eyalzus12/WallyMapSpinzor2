using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnim : IDeserializable, ISerializable, IDrawable
{
    public string InstanceName{get; set;} = null!;
    public string AssetName{get; set;} = null!;
    public double X{get; set;}
    public double Y{get; set;}
    
    //never actually used.
    public bool Foreground => InstanceName.StartsWith("am_Foreground");
    //never actually used.
    public bool Background => InstanceName.StartsWith("am_Background");

    //the next 3 are all added to some list. unclear what the list is relevant for.

    //indicates moving platform. format: am_Platform_[PlatID]
    public bool Platform => InstanceName.StartsWith("am_Platform");
    //indicates brawlball goal. if InstanceName contains "1", it is goal 1. otherwise goal 2.
    public bool BrawlGoal => InstanceName.StartsWith("am_BrawlGoal");
    //supposedly related to platform king, but code doesn't reveal much.
    public bool ColorPlatform => InstanceName.StartsWith("am_ColorPlatform");


    public void Deserialize(XElement e)
    {
        InstanceName = e.GetAttribute("InstanceName");
        AssetName = e.GetAttribute("AssetName");
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("InstanceName", InstanceName);
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
    }

    #pragma warning disable 0162 //unreachable code warning
    public void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture
    {
        //LevelAnim requires more than one swf shape to be rendered.
        //it's more work to get working, so for now im commenting it out
        return;

        if(!config.ShowAssets) return;
        //NOTE: there may be some extra logic needed here. need to get renderer impl working to figure out.
        T texture = canvas.LoadTextureFromSWF("SFX_Level.swf", AssetName);
        canvas.DrawTexture(X, Y, texture, trans, DrawPriorityEnum.MIDGROUND);
    }
    #pragma warning restore 0162 //unreachable code warning
}
