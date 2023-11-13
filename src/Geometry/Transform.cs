namespace WallyMapSpinzor2;

/*
Transform matrix layout:
ScaleX      SkewX       TranslateX
SkewY       ScaleY      TranslateY
0           0           1
*/

public readonly record struct Transform(double ScaleX, double SkewX, double SkewY, double ScaleY, double TranslateX, double TranslateY)
{
    public static readonly Transform IDENTITY = new(1,0,0,1,0,0);
    public static readonly Transform ZERO = new(0,0,0,0,0,0);
    public static readonly Transform FLIP_X = new(-1,0,0,1,0,0);
    public static readonly Transform FLIP_Y = new(1,0,0,-1,0,0);
    
    public static Transform CreateTranslate(double x, double y) => IDENTITY with {TranslateX = x, TranslateY = y};
    public static Transform CreateScale(double scaleX, double scaleY) => IDENTITY with {ScaleX = scaleX, ScaleY = scaleY};
    public static Transform CreateSkew(double skewX, double skewY) => IDENTITY with {ScaleX = Math.Cos(skewY), SkewX = -Math.Sin(skewX), SkewY = Math.Sin(skewY), ScaleY = Math.Cos(skewX)};
    public static Transform CreateRotate(double rot) => CreateSkew(rot, rot);
    public static Transform CreateFrom(double x = 0, double y = 0, double rot = 0, double skewX = 0, double skewY = 0, double scaleX = 1, double scaleY = 1) =>
        CreateTranslate(x, y) *
        CreateRotate(rot) *
        CreateSkew(skewX, skewY) *
        CreateScale(scaleX, scaleY);
    
    //I hope this is correct
    public static Transform operator *(Transform t1, Transform t2) => new(
        t1.ScaleX * t2.ScaleX + t1.SkewX * t2.SkewY,
        t1.ScaleX * t2.SkewX + t1.SkewX * t2.ScaleY,
        t1.SkewY * t2.ScaleX + t1.ScaleY * t2.SkewY,
        t1.SkewY * t2.SkewX + t1.ScaleY * t2.ScaleY,
        t1.ScaleX * t2.TranslateX + t1.SkewX * t2.TranslateY + t1.TranslateX,
        t1.SkewY * t2.TranslateX + t1.ScaleY * t2.TranslateY + t1.TranslateY
    );

    public static Position operator *(Transform t, Position p) => new(
        t.ScaleX * p.X + t.SkewX * p.Y + t.TranslateX,
        t.SkewY * p.X + t.ScaleY * p.Y + t.TranslateY
    );

    public static Transform operator *(Transform t, double f) => new
    (
        t.ScaleX*f, t.SkewX*f,
        t.SkewY*f, t.ScaleY*f,
        t.TranslateX*f, t.TranslateY*f
    );

    public double Determinant => ScaleX * ScaleY - SkewX * SkewY;
}