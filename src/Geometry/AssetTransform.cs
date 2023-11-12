namespace WallyMapSpinzor2;

public readonly record struct AssetTransform(double X, double Y, double Rotation, double SkewX, double SkewY, double ScaleX, double ScaleY)
{
    public static readonly AssetTransform IDENTITY = new(0,0,0,0,0,1,1);

    //I hope this works
    public static explicit operator Transform(AssetTransform ass) =>
        Transform.CreateTranslate(ass.X, ass.Y) *
        Transform.CreateRotate(ass.Rotation) *
        Transform.CreateSkew(ass.SkewX, ass.SkewY) *
        Transform.CreateScale(ass.ScaleX, ass.ScaleY);
}