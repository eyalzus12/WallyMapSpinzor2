using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LavaCollision : AbstractCollision
{
    public string LavaPower { get; set; } = null!;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        LavaPower = e.GetAttribute("LavaPower");
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("LavaPower", LavaPower);
        base.Serialize(e);
    }

    public override Color GetColor(RenderConfig config) => config.ColorLavaCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.GAMEMODE | CollisionTypeFlags.LAVA;
}