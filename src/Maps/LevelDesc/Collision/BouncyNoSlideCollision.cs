namespace WallyMapSpinzor2;

public class BouncyNoSlideCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorBouncyNoSlideCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.NO_SLIDE | CollisionTypeFlags.BOUNCY;
}