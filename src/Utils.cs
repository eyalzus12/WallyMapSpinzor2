using System.Globalization;

namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseFloatOrNull(string? s) => (s is null) ? null : double.Parse(s, CultureInfo.InvariantCulture);
    public static bool? ParseBoolOrNull(string? s) => (s is null) ? null : (s.ToUpperInvariant() == "TRUE");
    public static int? ParseIntOrNull(string? s) => (s is null) ? null : int.Parse(s, CultureInfo.InvariantCulture);
    public static uint? ParseUIntOrNull(string? s) => (s is null) ? null : (s.StartsWith("0x") ? Convert.ToUInt32(s, 16) : uint.Parse(s, CultureInfo.InvariantCulture));

    public static void DrawArrow<T>(this ICanvas<T> canvas, double x1, double y1, double x2, double y2, double arrowSide, double arrowBack, Color color, Transform trans, DrawPriorityEnum priority)
        where T : ITexture
    {
        canvas.DrawLine(x1, y1, x2, y2, color, trans, priority);
        // draw arrow parts
        // we start with an arrow pointing right
        // and we rotate it to match
        double length = BrawlhallaMath.Length(x2 - x1, y2 - y1); // arrow length
        (double dirX, double dirY) = BrawlhallaMath.Normalize(x2 - x1, y2 - y1); // arrow direction
        double angle = Math.Atan2(dirY, dirX); // arrow angle
        // calculate end points by applying the rotation to the arrow
        (double arrowEndX1, double arrowEndY1) = BrawlhallaMath.Rotated(length - arrowBack, arrowSide, angle);
        (double arrowEndX2, double arrowEndY2) = BrawlhallaMath.Rotated(length - arrowBack, -arrowSide, angle);
        // draw the lines
        canvas.DrawLine(x2, y2, x1 + arrowEndX1, y1 + arrowEndY1, color, trans, priority);
        canvas.DrawLine(x2, y2, x1 + arrowEndX2, y1 + arrowEndY2, color, trans, priority);
    }
}
