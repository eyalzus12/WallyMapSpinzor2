namespace WallyMapSpinzor2;

public record struct Line(float X1, float Y1, float X2, float Y2)
{
    public static readonly Line ZERO = new Line(0,0,0,0);
    public Position From => new(X1,Y1);
    public Position To => new(X2,Y2);
    public Position Middle => (From+To)/2;

    public override string ToString() => $"[{From},{To}]";
}