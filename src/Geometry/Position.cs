namespace WallyMapSpinzor2;

public readonly record struct Position(double X, double Y)
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
    public static Position operator *(Position p, double f) =>
        new(p.X * f, p.Y * f);
    public static Position operator *(double f, Position p) =>
        new(f * p.X, f * p.Y);
    public static Position operator /(Position p, double f) =>
        new(p.X / f, p.Y / f);
    
    public Position DirTo(Position p) => p - this;
    public Line LineTo(Position p) => new Line(X,Y,p.X,p.Y);
    public Rect RectTo(Position p) => new Rect(X,Y,p.X-X,p.Y-Y);

    public Position Lerp(Position p, double f) => this*(1-f) + p*f;

    public double Dot(Position p) => X*p.X + Y*p.Y;
    public double Cross(Position p) => X*p.Y - Y*p.X;

    public Position Abs => new(Math.Abs(X), Math.Abs(Y));

    public double LengthSquared => X*X + Y*Y;
    public double Length => Math.Sqrt(LengthSquared);
    public Position Normalized
    {get
    {
        double len = Length;
        if(len == 0) return Position.ZERO;
        return this / len;
    }}

    public double DistanceTo(Position p) => DirTo(p).Length;
    public double AngleTo(Position p) => Math.Atan2(this.Cross(p), this.Dot(p));
    public Position Rotated(double f)
    {
        double sine = Math.Sin(f); double cosi = Math.Cos(f);
        return new(X*cosi - Y*sine, X*sine + Y*cosi);
    }

    //ASSUMES BOTH VECTORS ARE NORMALIZED
    public Position Slerp(Position p, double weight) => this.Rotated(this.AngleTo(p) * weight);

    public Position LerpWithCenter(Position p, Position center, double weight)
    {
        Position nt = (this-center).Normalized;
        Position np = (p-center).Normalized;
        Position lp = nt.Slerp(np, weight);
        Position df = (this-p).Abs;
        return df*lp + center;
    }
}