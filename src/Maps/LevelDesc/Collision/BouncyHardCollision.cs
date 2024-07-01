namespace WallyMapSpinzor2;

public class BouncyHardCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorBouncyHardCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.BOUNCY;
}