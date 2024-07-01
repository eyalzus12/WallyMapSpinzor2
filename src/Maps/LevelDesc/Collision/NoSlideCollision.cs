namespace WallyMapSpinzor2;

public class NoSlideCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorNoSlideCollision;
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.NO_SLIDE;
}