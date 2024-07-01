namespace WallyMapSpinzor2;

public class ItemIgnoreCollision : AbstractCollision
{
    public override Color GetColor(RenderConfig config) => config.ColorItemIgnoreCollision;
    //yes, item ignore collision is a no-slide collision
    public override CollisionTypeFlags CollisionType => CollisionTypeFlags.HARD | CollisionTypeFlags.NO_SLIDE | CollisionTypeFlags.ITEM_IGNORE;
}