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

    public void StoreOffset(GlobalRenderData rd, double time)
    {
        (double _X, double _Y) = Animation.GetOffset(rd, time);
        //for some reason, dynamics need the first keyframe position of the animation removed
        (double _AX, double _AY) = Animation.KeyFrames[0].GetPosition();
        rd.PlatIDDict[PlatID] = (_X - _AX, _Y - _AY);
    }

    public override void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time) 
    {
        if(!rd.PlatIDDict.ContainsKey(PlatID))
            throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw MovingPlatform. Make sure to call StoreOffset beforehand.");

        (double _AX, double _AY) = Animation.KeyFrames[0].GetPosition();
        (double _X, double _Y) = rd.PlatIDDict[PlatID];
        Transform tt = t * Transform.CreateTranslate(X + _X + _AX, Y + _Y + _AY);
        foreach(AbstractAsset a in Assets)
            a.DrawOn(canvas, rd, rs, tt, time);
    }
}