using System.Xml.Linq;

namespace WallyMapSpinzor2;

// for some godforsaken reason, MudCollision actually does nothing, instead applying global values
// this object is created from the LevelDesc, and exists for convenience
public sealed class LevelMud : IDeserializable<LevelMud>, ISerializable, IDrawable
{
    private const double MISSING_MUD_Y_VALUE = 9999;
    public double MudY { get; set; } = MISSING_MUD_Y_VALUE;
    public double MudFallMult { get; set; } = 0.1;
    public double MudFallStunMult { get; set; } = 0.1;
    public double MudXSpeedMult { get; set; } = 0.2;
    public double MudKillDepth { get; set; } = 150;
    public bool MudJumpBack { get; set; } = true;

    public bool IsValid => MudY != MISSING_MUD_Y_VALUE;

    public LevelMud() { }
    private LevelMud(XElement e)
    {
        foreach (XElement mud in e.Elements("MudCollision"))
        {
            MudY = mud.GetDoubleAttribute("Y", 0);
            MudFallMult = mud.GetDoubleAttribute("MudFallMult", MudFallMult);
            MudFallStunMult = mud.GetDoubleAttribute("MudFallStunMult", MudFallStunMult);
            MudXSpeedMult = mud.GetDoubleAttribute("MudXSpeedMult", MudXSpeedMult);
            MudKillDepth = mud.GetDoubleAttribute("MudKillDepth", MudKillDepth);
            MudJumpBack = mud.GetBoolAttribute("MudJumpBack", MudJumpBack);
        }
    }
    public static LevelMud Deserialize(XElement e) => new(e);

    public void Serialize(XElement e)
    {
        if (!IsValid) return;

        // bullshit X1,X2 values for the game's sake
        e.SetAttributeValue("X1", -5000);
        e.SetAttributeValue("X2", 5000);
        e.SetAttributeValue("Y", MudY);
        // to prevent issues with normals
        e.SetAttributeValue("NormalY", -1);

        e.SetAttributeValue("MudFallMult", MudFallMult);
        e.SetAttributeValue("MudFallStunMult", MudFallStunMult);
        e.SetAttributeValue("MudXSpeedMult", MudXSpeedMult);
        e.SetAttributeValue("MudKillDepth", MudKillDepth);
        e.SetAttributeValue("MudJumpBack", MudJumpBack.ToString().ToLowerInvariant());
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!IsValid || !config.ShowMudLine) return;

        canvas.DrawLine(-5000, MudY, 5000, MudY, config.ColorMudLine, trans, DrawPriorityEnum.DATA, this);
        canvas.DrawLine(-5000, MudY + MudKillDepth, 5000, MudY + MudKillDepth, config.ColorMudKillLine, trans, DrawPriorityEnum.DATA, this);
    }
}