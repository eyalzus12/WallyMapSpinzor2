using System;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractPressurePlateCollision : AbstractCollision
{
    public double AnimOffsetX { get; set; }
    public double AnimOffsetY { get; set; }
    public double AnimRotation { get; set; }
    public string AssetName { get; set; } = null!;
    public int Cooldown { get; set; }
    public bool FaceLeft { get; set; }
    public double[] FireOffsetX { get; set; } = null!;
    public double[] FireOffsetY { get; set; } = null!;
    public string? PlatID { get; set; }
    public string[] TrapPowers { get; set; } = null!;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        AnimOffsetX = e.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = e.GetFloatAttribute("AnimOffsetY", 0);
        AnimRotation = e.GetFloatAttribute("AnimRotation");
        AssetName = e.GetAttribute("AssetName");
        Cooldown = e.GetIntAttribute("Cooldown", 3000);
        FaceLeft = e.GetBoolAttribute("FaceLeft", false);
        FireOffsetX = [.. e.GetAttribute("FireOffsetX").Split(',').Select(double.Parse)];
        FireOffsetY = [.. e.GetAttribute("FireOffsetY").Split(',').Select(double.Parse)];
        if (FireOffsetY.Length == 0) FireOffsetY = [-10]; //the game defaults it to -10
        PlatID = e.GetAttributeOrNull("PlatID");
        TrapPowers = e.GetAttribute("TrapPowers").Split(',');
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("AnimOffsetX", AnimOffsetX);
        e.SetAttributeValue("AnimOffsetY", AnimOffsetY);
        if (AnimRotation != 0)
            e.SetAttributeValue("AnimRotation", AnimRotation);
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("Cooldown", Cooldown);
        e.SetAttributeValue("FaceLeft", FaceLeft.ToString().ToLowerInvariant());
        e.SetAttributeValue("FireOffsetX", string.Join(',', FireOffsetX));
        e.SetAttributeValue("FireOffsetY", string.Join(',', FireOffsetY));
        if (PlatID is not null)
            e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("TrapPowers", string.Join(',', TrapPowers));
        base.Serialize(e);
    }

    public override void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        base.DrawOn(canvas, trans, config, context, state);

        // if a pressure plate has an Anchor, its
        // pressureplate-ness is ignored by the game.
        if (AnchorX is not null || AnchorY is not null)
            return;

        for (int i = 0; i < TrapPowers.Length; ++i)
        {
            double fireOffsetX = GetOffset(FireOffsetX, i);
            double fireOffsetY = GetOffset(FireOffsetY, i);
            if (config.ShowFireOffsetLocation)
                canvas.DrawCircle(fireOffsetX, fireOffsetY, config.RadiusFireOffsetLocation, config.ColorFireOffset, trans, DrawPriorityEnum.FIRE_OFFSET, this);
            if (config.ShowFireOffsetLine)
                canvas.DrawLine((X1 + X2) / 2, (Y1 + Y2) / 2, fireOffsetX, fireOffsetY, config.ColorFireOffsetLine, trans, DrawPriorityEnum.FIRE_OFFSET, this);
            if (config.ShowFireDirection)
            {
                double arrowEndX = fireOffsetX + (FaceLeft ? -1 : 1) * config.LengthFireDirectionArrow;
                double arrowEndY = fireOffsetY;
                canvas.DrawArrow(fireOffsetX, fireOffsetY, arrowEndX, arrowEndY, config.OffsetFireDirectionArrowSide, config.OffsetFireDirectionArrowBack, config.ColorFireDirection, trans, DrawPriorityEnum.FIRE_OFFSET, this);
            }
        }

        if (config.ShowAssets)
        {
            if (PlatID is not null && !context.PlatIDMovingPlatformOffset.ContainsKey(PlatID))
                throw new InvalidOperationException($"Plat ID dictionary did not contain plat id {PlatID} when attempting to draw pressure plate. Make sure to call {nameof(MovingPlatform.StoreMovingPlatformOffset)}.");
            (double platformX, double platformY) = (PlatID is null) ? (0, 0) : context.PlatIDMovingPlatformOffset[PlatID];
            double assetX = platformX + AnimOffsetX;
            double assetY = platformY + AnimOffsetY;
            Transform spriteTrans = Transform.CreateFrom(x: assetX, y: assetY, rot: AnimRotation * Math.PI / 180);
            Gfx gfx = new()
            {
                AnimFile = "Animation_GameModes.swf",
                AnimClass = "a__AnimationPressurePlate",
                AnimScale = 1,
                FireAndForget = true,
                CustomArts = AssetName.Length > 26 ? [new CustomArt() { FileName = "Animation_GameModes.swf", Name = AssetName[25..] }] : [],
            };
            canvas.DrawAnim(gfx, "Ready", 0, spriteTrans, DrawPriorityEnum.MIDGROUND, this);
        }
    }

    // this is how it's done ingame
    private double GetOffset(double[] offset, int i)
    {
        if (offset.Length == 0)
            return 0;
        else if (offset.Length == TrapPowers.Length)
            return offset[i];
        else
            return offset[0];
    }
}