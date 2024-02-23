namespace WallyMapSpinzor2;

public class BouncyNoSlideCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorBouncyNoSlideCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.NO_SLIDE | CollisionTypeEnum.BOUNCY;
}