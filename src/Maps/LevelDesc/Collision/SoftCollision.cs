namespace WallyMapSpinzor2;

public class SoftCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorSoftCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.SOFT;
}