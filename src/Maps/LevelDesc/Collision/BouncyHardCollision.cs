namespace WallyMapSpinzor2;

public class BouncyHardCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorBouncyHardCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.BOUNCY;
}
