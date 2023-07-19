namespace WallyMapSpinzor2;

public record struct Position(float X, float Y)
{
    public static readonly Position ZERO = new(0,0);

    public override string ToString() => $"({X}, {Y})";

    public static Position operator +(Position p) =>
        new(p.X, p.Y);
    public static Position operator -(Position p) =>
        new(-p.X, -p.Y);
    public static Position operator +(Position p1, Position p2) =>
        new(p1.X + p2.X, p1.Y + p2.Y);
    public static Position operator -(Position p1, Position p2) =>
        new(p1.X - p2.X, p1.Y - p2.Y);
    public static Position operator *(Position p1, Position p2) =>
        new(p1.X * p2.X, p1.Y * p2.Y);
    public static Position operator /(Position p1, Position p2) =>
        new(p1.X / p2.X, p1.Y / p2.Y);
    public static Position operator *(Position p, float f) =>
        new(p.X * f, p.Y * f);
    public static Position operator *(float f, Position p) =>
        new(f * p.X, f * p.Y);
    public static Position operator /(Position p, float f) =>
        new(p.X / f, p.Y / f);
    
    public Position DirTo(Position p) => p - this;
    public Line LineTo(Position p) => new Line(X,Y,p.X,p.Y);
    public Rect RectTo(Position p) => new Rect(X,Y,p.X-X,p.Y-Y);

    public Position Lerp(Position p, float f) => this*(1-f) + p*f;

    public float Length => MathF.Sqrt(X*X + Y*Y);
    public Position Normalized
    {get
    {
        float len = Length;
        if(len == 0) return Position.ZERO;
        return this / len;
    }}

    public float DistanceTo(Position p) => DirTo(p).Length;
}