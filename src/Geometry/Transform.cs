namespace WallyMapSpinzor2;

/*
Transform matrix layout:
ScaleX      RotateSkew0 TranslateX
RotateSkew1 ScaleY      TranslateY
0           0           1
*/

public readonly record struct Transform(double ScaleX, double RotateSkew0, double RotateSkew1, double ScaleY, double TranslateX, double TranslateY)
{
    public static readonly Transform IDENTITY = new(1,0,0,1,0,0);
    public static readonly Transform ZERO = new(0,0,0,0,0,0);
    public static readonly Transform FLIP_X = new(-1,0,0,1,0,0);
    public static readonly Transform FLIP_Y = new(1,0,0,-1,0,0);

    public static Transform CreateTranslate(double x, double y) => IDENTITY with {TranslateX = x, TranslateY = y};
    public static Transform CreateScale(double scaleX, double scaleY) => IDENTITY with {ScaleX = scaleX, ScaleY = scaleY};
    public static Transform CreateRotate(double rot) => IDENTITY with {ScaleX = Math.Cos(rot), RotateSkew0 = Math.Sin(rot), RotateSkew1 = Math.Sin(rot), ScaleY = Math.Cos(rot)};
    
    //I hope this is correct
    public static Transform operator *(Transform t1, Transform t2) => new(
        t1.ScaleX * t2.ScaleX + t1.RotateSkew0 * t2.RotateSkew1,
        t1.ScaleX * t2.RotateSkew0 + t1.RotateSkew0 * t2.ScaleY,
        t1.RotateSkew1 * t2.ScaleX + t1.ScaleY * t2.RotateSkew1,
        t1.RotateSkew1 * t2.RotateSkew0 + t1.ScaleY * t2.ScaleY,
        t1.ScaleX * t2.TranslateX + t1.RotateSkew0 * t2.TranslateY + t1.TranslateX,
        t1.RotateSkew1 * t2.TranslateX + t1.ScaleY * t2.TranslateY + t1.TranslateY
    );

    public static Position operator *(Transform t, Position p) => new(
        t.ScaleX * p.X + t.RotateSkew0 * p.Y + t.TranslateX,
        t.RotateSkew1 * p.X + t.ScaleY * p.Y + t.TranslateY
    );

    public static Transform operator *(Transform t, double f) => new
    (
        t.ScaleX*f, t.RotateSkew0*f,
        t.RotateSkew1*f, t.ScaleY*f,
        t.TranslateX*f, t.TranslateY*f
    );
}