namespace WallyMapSpinzor2;

public class NoSlideCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorNoSlideCollision;
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.NO_SLIDE;
}
