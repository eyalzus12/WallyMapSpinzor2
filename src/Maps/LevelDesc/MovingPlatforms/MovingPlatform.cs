using System.Xml.Linq;

namespace WallyMapSpinzor2;

/*
yes, moving platforms can technically have the stuff you'd normally expect from a Platform
if they have an AssetName, then they'd act like a Platform, and won't do anything with Animation
but that would never be done ingame, so we just ignore AssetName when drawing.
and yes the game does technically support MovingPlatform inside a Platform or MovingPlatform.
don't question it.

now, the real reason for making MovingPlatform an AbstractAsset is that it's possible for the game
to put its MovingPlatforms AFTER Platforms, which would alter the drawing order.
this is only done in BP8ThreePlatformFFABig, but we have to support it.

thanks bmg.
*/
public class MovingPlatform : AbstractAsset
{
    public string PlatID{get; set;} = null!;
    public Animation Animation{get; set;} = null!;
    public List<AbstractAsset> Assets{get; set;} = null!;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        PlatID = e.GetAttribute("PlatID");
        //Animation is always supposed to exist
        //The game technically supports it not existing
        //In which case the moving platform doesn't exist
        Animation = e.DeserializeChildOfType<Animation>()!;
        Assets = e.DeserializeAssetChildren();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("PlatID", PlatID);
        base.Serialize(e);
        e.Add(Animation.SerializeToXElement());
        foreach(AbstractAsset a in Assets)
            e.Add(a.SerializeToXElement());
    }

    public void StoreOffset(RenderData rd, TimeSpan time)
    {
        (double offX, double offY) = Animation.GetOffset(rd, time);
        //for some reason, dynamics need the first keyframe position of the animation removed
        (double anmX, double anmY) = Animation.KeyFrames[0].GetPosition();
        rd.PlatIDDynamicOffset[PlatID] = (offX - anmX, offY - anmY);
        rd.PlatIDMovingPlatformOffset[PlatID] = (offX + X, offY + Y);
    }

    public override void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data) 
    {
        if(!data.PlatIDMovingPlatformOffset.ContainsKey(PlatID))
            throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw MovingPlatform. Make sure to call StoreOffset beforehand.");

        (double offX, double offY) = data.PlatIDMovingPlatformOffset[PlatID];
        Transform childTrans = trans * Transform.CreateTranslate(offX, offY);
        foreach(AbstractAsset a in Assets)
            a.DrawOn(canvas, config, childTrans, time, data);
    }
}
