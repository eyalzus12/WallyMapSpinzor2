using System;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class AnimatedBackground : IDeserializable, ISerializable, IDrawable
{
    public bool Midground { get; set; }

    public Gfx Gfx { get; set; } = null!;

    public double Position_X { get; set; }
    public double Position_Y { get; set; }

    public double Skew_X { get; set; }
    public double Skew_Y { get; set; }

    public double Scale_X { get; set; }
    public double Scale_Y { get; set; }

    public double Rotation { get; set; }

    public int FrameOffset { get; set; }

    public bool ForceDraw { get; set; }

    public uint Loops { get; set; } // 0 means infinite

    public void Deserialize(XElement e)
    {
        Midground = e.GetBoolAttribute("Midground", false);

        Gfx = e.DeserializeRequiredChildOfType<Gfx>();

        string[]? position = e.GetElementOrNull("Position")?.Split(',', 2);
        Position_X = Utils.ParseDoubleOrNull(position?[0]) ?? 0;
        Position_Y = Utils.ParseDoubleOrNull(position?[1]) ?? 0;

        string[]? skew = e.GetElementOrNull("Skew")?.Split(',', 2);
        Skew_X = Utils.ParseDoubleOrNull(skew?[0]) ?? 0;
        Skew_Y = Utils.ParseDoubleOrNull(skew?[1]) ?? 0;

        string[]? scale = e.GetElementOrNull("Scale")?.Split(',', 2);
        Scale_X = Utils.ParseDoubleOrNull(scale?[0]) ?? 1;
        Scale_Y = Utils.ParseDoubleOrNull(scale?[1]) ?? 1;

        Rotation = e.GetDoubleElement("Rotation", 0);
        FrameOffset = e.GetIntElement("FrameOffset", 0);
        ForceDraw = e.GetBoolElement("ForceDraw", false);
        Loops = e.GetUIntElement("Loops", 0);
    }

    public void Serialize(XElement e)
    {
        if (Midground)
            e.SetAttributeValue("Midground", "true");

        e.AddSerialized(Gfx);

        e.AddChild("Position", $"{Position_X},{Position_Y}");

        if (Rotation == 0 || Skew_X != 0 || Skew_Y != 0)
            e.AddChild("Skew", $"{Skew_X},{Skew_Y}");

        e.AddChild("Scale", $"{Scale_X},{Scale_Y}");

        if (Rotation != 0)
            e.AddChild("Rotation", Rotation);

        if (FrameOffset != 0)
            e.AddChild("FrameOffset", FrameOffset);

        if (ForceDraw)
            e.AddChild("ForceDraw", "True");

        if (Loops != 0)
            e.AddChild("Loops", Loops);
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.AnimatedBackgrounds && !ForceDraw)
            return;
        long frame = LevelDesc.GET_ANIM_FRAME(config.Time) + FrameOffset;

        // Non-midground animated backgrounds are BACKGROUNDS, so they need to be transformed to match the background.
        DrawPriorityEnum priority = Midground ? DrawPriorityEnum.MIDGROUND : DrawPriorityEnum.BACKGROUND;
        Transform spriteTrans = (Midground ? Transform.IDENTITY : CalculateBackgroundTransform(context)) * SpriteTransform;
        canvas.DrawAnim(Gfx, "Ready", frame, trans * spriteTrans, priority, this, loopLimit: Loops == 0 ? null : Loops);
    }

    private static Transform CalculateBackgroundTransform(RenderContext context)
    {
        double backgroundX = context.BackgroundRect_X!.Value;
        double backgroundY = context.BackgroundRect_Y!.Value;
        double backgroundScaleX = context.BackgroundRect_W!.Value / context.CurrentBackground!.W;
        double backgroundScaleY = context.BackgroundRect_H!.Value / context.CurrentBackground!.H;
        return Transform.CreateFrom(x: backgroundX, y: backgroundY, scaleX: backgroundScaleX, scaleY: backgroundScaleY);
    }

    public Transform SpriteTransform => Transform.CreateFrom(
        x: Position_X,
        y: Position_Y,
        rot: Rotation * Math.PI / 180,
        skewX: Skew_X * Math.PI / 180,
        skewY: Skew_Y * Math.PI / 180,
        scaleX: Scale_X,
        scaleY: Scale_Y
    );
}