namespace WallyMapSpinzor2;

public readonly record struct AssetTransform(float X, float Y, float ScaleX, float ScaleY, float Rotation)
{
    public static readonly AssetTransform IDENTITY = new AssetTransform(0,0,1,1,0);

    //I hope this works
    public static explicit operator Transform(AssetTransform ass) =>
        Transform.CreateTranslate(ass.X, ass.Y) *
        Transform.CreateScale(ass.ScaleX, ass.ScaleY) *
        Transform.CreateRotate(ass.Rotation);
}