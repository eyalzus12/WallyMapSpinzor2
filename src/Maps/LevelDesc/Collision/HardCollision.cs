namespace WallyMapSpinzor2;

public class HardCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorHardCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD;
}