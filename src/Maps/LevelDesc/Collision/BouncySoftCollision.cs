namespace WallyMapSpinzor2;

public class BouncySoftCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorBouncySoftCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.SOFT | CollisionTypeEnum.BOUNCY;
}