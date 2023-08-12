namespace WallyMapSpinzor2;

public readonly record struct Rect(double X, double Y, double W, double H)
{
    public static readonly Rect ZERO = new(0,0,0,0);

    public Position TopLeft => new(X,Y);
    public Position Size => new(W,H);
    public Position BottomRight => TopLeft + Size;
    public Position TopRight => new(BottomRight.X, TopLeft.Y);
    public Position BottomLeft => new(TopLeft.X, BottomRight.Y);
    public Position Center => (TopLeft + BottomRight)/2;
}