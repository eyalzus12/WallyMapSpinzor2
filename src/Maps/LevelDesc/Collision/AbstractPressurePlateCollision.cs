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
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10}; //the game defaults it to -10
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

    public override void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
    {
        base.DrawOn(canvas, config, trans, time, data);

        //if a pressure plate has an Anchor, its
        //pressureplate-ness is ignored by the game.
        if(AnchorX is not null || AnchorY is not null)
            return;
        
        //TODO: show fire offset

        if(config.ShowAssets)
        {
            if(PlatID is not null && !data.PlatIDMovingPlatformOffset.ContainsKey(PlatID))
                throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw pressure plate. Make sure to call StoreOffset beforehand.");
            
            string finalAssetName = AssetName;
            //that's how the game does this check. don't ask me.
            if(finalAssetName.Length > "a__AnimationPressurePlate".Length + 1)
                finalAssetName = finalAssetName["a__AnimationPressurePlate".Length..];
            
            T texture = canvas.LoadTextureFromSWF("bones/Bones_GameModes.swf", finalAssetName);

            //replace the dynamic transform with the moving platform's transform
            (double DynX, double DynY) = (PlatID is null)?(0, 0):data.PlatIDDynamicOffset[PlatID];
            (double _X, double _Y) = (PlatID is null)?(0, 0):data.PlatIDMovingPlatformOffset[PlatID];
            _X -= DynX; _Y -= DynY;
            
            _X += AnimOffsetX; _Y += AnimOffsetY;
            //for some reason brawlhalla further offsets the sprite by half its size
            _X -= texture.W / 2.0; _Y -= texture.H / 2.0;
            
            Transform tt = trans * Transform.CreateFrom(x : _X, y : _Y, rot : AnimRotation);
            canvas.DrawTexture(0, 0, texture, tt, DrawPriorityEnum.MIDGROUND);
        }
    }
}