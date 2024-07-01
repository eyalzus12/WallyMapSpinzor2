using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractCollision : IDeserializable, ISerializable, IDrawable
{
    public enum FlagEnum
    {
        DEFAULT = 0,
        WATER = 1,
        WATERBLUE = 2,
        ICE = 3,
        METAL = 4,
        WOOD = 5,
        PUDDLE = 6,
        ROPEBRIDGE = 7,
        SAND = 8
    }

    //brawlhalla doesn't define anything for this lol
    public enum ColorFlagEnum
    {
        DEFAULT = 0
    }

    public string? TauntEvent { get; set; }
    public int Team { get; set; }
    public double? AnchorX { get; set; }
    public double? AnchorY { get; set; }
    public double NormalX { get; set; }
    public double NormalY { get; set; }
    public double X1 { get; set; }
    public double X2 { get; set; }
    public double Y1 { get; set; }
    public double Y2 { get; set; }
    public FlagEnum? Flag { get; set; }
    public ColorFlagEnum? ColorFlag { get; set; }

    public DynamicCollision? Parent { get; set; }

    public virtual void Deserialize(XElement e)
    {
        TauntEvent = e.GetAttributeOrNull("TauntEvent");

        Team = e.GetIntAttribute("Team", 0);

        //brawlhalla requires both attributes to exist for an anchor
        AnchorX = AnchorY = null;
        if (e.HasAttribute("AnchorX") && e.HasAttribute("AnchorY"))
        {
            AnchorX = e.GetFloatAttribute("AnchorX");
            AnchorY = e.GetFloatAttribute("AnchorY");
        }

        NormalX = e.GetFloatAttribute("NormalX", 0);
        NormalY = e.GetFloatAttribute("NormalY", 0);

        X2 = X1 = 0;
        if (e.HasAttribute("X"))
        {
            X2 = X1 = e.GetFloatAttribute("X");
        }
        else if (e.HasAttribute("X1") && e.HasAttribute("X2"))
        {
            X1 = e.GetFloatAttribute("X1");
            X2 = e.GetFloatAttribute("X2");
        }

        Y2 = Y1 = 0;
        if (e.HasAttribute("Y"))
        {
            Y2 = Y1 = e.GetFloatAttribute("Y");
        }
        else if (e.HasAttribute("Y1") && e.HasAttribute("Y2"))
        {
            Y1 = e.GetFloatAttribute("Y1");
            Y2 = e.GetFloatAttribute("Y2");
        }

        Flag =
            e.HasAttribute("Flag")
            ? Enum.TryParse(e.GetAttribute("Flag").ToUpperInvariant(), out FlagEnum flag)
                ? flag
                : FlagEnum.DEFAULT
            : null;

        ColorFlag =
            e.HasAttribute("ColorFlag")
            ? Enum.TryParse(e.GetAttribute("ColorFlag")?.ToUpperInvariant(), out ColorFlagEnum colorFlag)
                ? colorFlag
                : ColorFlagEnum.DEFAULT
            : null;
    }

    public virtual void Serialize(XElement e)
    {
        if (TauntEvent is not null)
            e.SetAttributeValue("TauntEvent", TauntEvent);

        if (Team != 0)
            e.SetAttributeValue("Team", Team);

        if (AnchorX is not null && AnchorY is not null)
        {
            e.SetAttributeValue("AnchorX", AnchorX);
            e.SetAttributeValue("AnchorY", AnchorY);
        }

        if (NormalX != 0)
            e.SetAttributeValue("NormalX", NormalX);
        if (NormalY != 0)
            e.SetAttributeValue("NormalY", NormalY);

        if (X1 == X2)
        {
            e.SetAttributeValue("X", X1);
        }
        else
        {
            e.SetAttributeValue("X1", X1);
            e.SetAttributeValue("X2", X2);
        }

        if (Y1 == Y2)
        {
            e.SetAttributeValue("Y", Y1);
        }
        else
        {
            e.SetAttributeValue("Y1", Y1);
            e.SetAttributeValue("Y2", Y2);
        }

        if (Flag is not null)
            e.SetAttributeValue("Flag", Flag.Value.ToString().ToLowerInvariant());

        if (ColorFlag is not null)
            e.SetAttributeValue("ColorFlag", ColorFlag.Value.ToString().ToLowerInvariant());
    }

    public (double, double)[] CalculateCurve()
    {
        if (AnchorX is null || AnchorY is null)
        {
            return [(X1, Y1), (X2, Y2)];
        }
        double xOff = Parent?.X ?? 0;
        double yOff = Parent?.Y ?? 0;
        return [(X1, Y1), .. BrawlhallaMath.CollisionQuad(X1, Y1, X2, Y2, AnchorX.Value - xOff, AnchorY.Value - yOff)];
    }

    public virtual void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowCollision) return;

        (double, double)[] anchorCurve = CalculateCurve();

        for (int i = 0; i < anchorCurve.Length - 1; ++i)
        {
            (double prevX, double prevY) = anchorCurve[i];
            (double nextX, double nextY) = anchorCurve[i + 1];
            //draw current line
            if (Team == 0)
                canvas.DrawLine(prevX, prevY, nextX, nextY, GetColor(config), trans, DrawPriorityEnum.DATA, this);
            else
            {
                if (Team - 1 >= config.ColorCollisionTeam.Length)
                    throw new ArgumentOutOfRangeException($"Collision has team {Team} which is larger than max available collision team color {config.ColorCollisionTeam.Length}");
                canvas.DrawLineMultiColor(
                        prevX, prevY, nextX, nextY,
                        [config.ColorCollisionTeam[Team - 1], GetColor(config), config.ColorCollisionTeam[Team - 1]],
                        trans, DrawPriorityEnum.DATA,
                        this
                    );
            }

            //draw normal line (unless there's an Anchor - this follows the game)
            if (AnchorX is null && AnchorY is null && config.ShowCollisionNormalOverride)
            {
                if (NormalX != 0 || NormalY != 0)
                {
                    //normals are auto-normalized since patch 8.03
                    (double normalX, double normalY) = BrawlhallaMath.Normalize(NormalX, NormalY);
                    double normalStartX = (prevX + nextX) / 2;
                    double normalStartY = (prevY + nextY) / 2;
                    double normalEndX = normalStartX + config.LengthCollisionNormal * normalX;
                    double normalEndY = normalStartY + config.LengthCollisionNormal * normalY;

                    canvas.DrawLine(
                        normalStartX, normalStartY, normalEndX, normalEndY,
                        config.ColorCollisionNormal, trans, DrawPriorityEnum.DATA,
                        this
                    );
                }
                //Otherwise normals are auto generated
                //However this logic requires raycasting
                //We'll add this later if we need it
            }
        }
    }

    public abstract Color GetColor(RenderConfig config);

    [Flags]
    public enum CollisionTypeFlags
    {
        HARD = 1 << 0,
        SOFT = 1 << 1,
        TRIGGER = 1 << 2,
        STICKY = 1 << 3,
        NO_SLIDE = 1 << 4,
        ITEM_IGNORE = 1 << 5,
        BOUNCY = 1 << 6,
        GAMEMODE = 1 << 7,
        PRESSURE_PLATE = 1 << 8,
        LAVA = 1 << 9,
    }

    public abstract CollisionTypeFlags CollisionType { get; }
}