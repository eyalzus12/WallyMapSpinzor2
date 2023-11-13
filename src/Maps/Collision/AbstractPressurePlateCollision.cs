using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractPressurePlateCollision : AbstractCollision
{
    public double AnimOffsetX{get; set;}
    public double AnimOffsetY{get; set;}
    public double AnimRotation{get; set;}
    public string AssetName{get; set;} = null!;
    public int Cooldown{get; set;}
    public bool FaceLeft{get; set;}
    public List<double> FireOffsetX{get; set;} = null!;
    public List<double> FireOffsetY{get; set;} = null!;
    public string? PlatID{get; set;}
    public List<string> TrapPowers{get; set;} = null!;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        AnimOffsetX = e.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = e.GetFloatAttribute("AnimOffsetY", 0);
        AnimRotation = e.GetFloatAttribute("AnimRotation");
        AssetName = e.GetAttribute("AssetName");
        Cooldown = e.GetIntAttribute("Cooldown", 3000);
        FaceLeft = e.GetBoolAttribute("FaceLeft", false);
        FireOffsetX = e.GetAttribute("FireOffsetX").Split(',').Select(double.Parse).ToList();
        FireOffsetY = e.GetAttribute("FireOffsetY").Split(',').Select(double.Parse).ToList();
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10}; //wtf bmg
        PlatID = e.GetNullableAttribute("PlatID");
        TrapPowers = e.GetAttribute("TrapPowers").Split(',').ToList();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("AnimOffsetX", AnimOffsetX.ToString());
        e.SetAttributeValue("AnimOffsetY", AnimOffsetY.ToString());
        if(AnimRotation != 0)
            e.SetAttributeValue("AnimRotation", AnimRotation.ToString());
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("Cooldown", Cooldown.ToString());
        e.SetAttributeValue("FaceLeft", FaceLeft.ToString().ToLower());
        e.SetAttributeValue("FireOffsetX", string.Join(',', FireOffsetX));
        e.SetAttributeValue("FireOffsetY", string.Join(',', FireOffsetY));
        if(PlatID is not null)
            e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("TrapPowers", string.Join(',', TrapPowers));
        base.Serialize(e);
    }

    public override void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
    {
        base.DrawOn(canvas, rd, rs, t, time);
        if(rs.ShowAssets)
        {
            if(PlatID is not null && !rd.PlatIDMovingPlatformOffset.ContainsKey(PlatID))
                throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw pressure plate. Make sure to call StoreOffset beforehand.");
            TTexture texture = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", AssetName);
            (double _X, double _Y) = (PlatID is null)?(0, 0):rd.PlatIDMovingPlatformOffset[PlatID];
            _X += AnimOffsetX; _Y += AnimOffsetY;
            //for some reason brawlhalla further offsets the sprite by half its size
            _X -= texture.W / 2; _Y -= texture.H / 2;

            /*
            WARNING:
            the asset transform for pressure plates is different from its collision transform. 
            the moving platform's platID is used instead of the parent Dynamic's.
            this means that we have to ignore the given draw transform.

            hopefully this won't cause any issues in the future...
            */
            Transform tt = Transform.CreateFrom(x : _X, y : _Y, rot : AnimRotation);
            canvas.DrawTexture(0, 0, texture, tt, DrawPriorityEnum.MIDGROUND);
        }
    }
}