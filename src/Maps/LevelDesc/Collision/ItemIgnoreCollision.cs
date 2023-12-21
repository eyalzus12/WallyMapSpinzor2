namespace WallyMapSpinzor2;

public class ItemIgnoreCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorItemIgnoreCollision;
    //yes, item ignore collision is a no-slide collision
    public override CollisionTypeEnum CollisionType => CollisionTypeEnum.HARD | CollisionTypeEnum.NO_SLIDE | CollisionTypeEnum.ITEM_IGNORE;
}
