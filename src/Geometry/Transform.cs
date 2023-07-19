namespace WallyMapSpinzor2;

/*
Transform matrix layout:
ScaleX      RotateSkew0 TranslateX
RotateSkew1 ScaleY      TranslateY
0           0           1
*/

public record struct Transform(float ScaleX, float RotateSkew0, float RotateSkew1, float ScaleY, float TranslateX, float TranslateY)
{
    public static readonly Transform IDENTITY = new(1,0,0,1,0,0);
    public static readonly Transform ZERO = new(0,0,0,0,0,0);
    public static readonly Transform FLIP_X = new(-1,0,0,1,0,0);
    public static readonly Transform FLIP_Y = new(1,0,0,-1,0,0);

    public Transform CreateTranslate(float x, float y) => IDENTITY with {TranslateX = x, TranslateY = y};
    public Transform CreateScale(float scaleX, float scaleY) => IDENTITY with {ScaleX = scaleX, ScaleY = scaleY};
    public Transform CreateRotate(float rot) => IDENTITY with {ScaleX = MathF.Cos(rot), RotateSkew0 = MathF.Sin(rot), RotateSkew1 = MathF.Sin(rot), ScaleY = MathF.Cos(rot)};
    
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

    public static Transform operator *(Transform t, float f) => new
    (
        t.ScaleX*f, t.RotateSkew0*f,
        t.RotateSkew1*f, t.ScaleY*f,
        t.TranslateX*f, t.TranslateY*f
    );
}