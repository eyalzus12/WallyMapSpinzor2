namespace WallyMapSpinzor2;

public readonly record struct Line(double X1, double Y1, double X2, double Y2)
{
    public static readonly Line ZERO = new(0,0,0,0);
    public Position From => new(X1,Y1);
    public Position To => new(X2,Y2);
    public Position Middle => (From+To)/2;

    public override string ToString() => $"[{From},{To}]";
}