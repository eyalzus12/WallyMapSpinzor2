using System;
using System.Globalization;

namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseDoubleOrNull(string? s) => (s is null) ? null : double.Parse(s, CultureInfo.InvariantCulture);
    public static float? ParseFloatOrNull(string? s) => (s is null) ? null : float.Parse(s, CultureInfo.InvariantCulture);
    public static bool? ParseBoolOrNull(string? s) => s?.Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
    public static int? ParseIntOrNull(string? s) => (s is null) ? null : int.Parse(s, CultureInfo.InvariantCulture);
    public static uint? ParseUIntOrNull(string? s) => (s is null) ? null : (s.StartsWith("0x") ? Convert.ToUInt32(s, 16) : uint.Parse(s, CultureInfo.InvariantCulture));
    public static E ParseEnumOrDefault<E>(string? s, E @default = default) where E : struct, Enum => Enum.TryParse(s, out E e) ? e : @default;
    public static E? ParseEnumOrNull<E>(string? s, E @default = default) where E : struct, Enum => s is null ? null : Enum.TryParse(s, out E e) ? e : @default;

    public static void DrawArrow(this ICanvas canvas, double x1, double y1, double x2, double y2, double arrowSide, double arrowBack, Color color, Transform trans, DrawPriorityEnum priority, object? caller)
    {
        canvas.DrawLine(x1, y1, x2, y2, color, trans, priority, caller);
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
        canvas.DrawLine(x2, y2, x1 + arrowEndX1, y1 + arrowEndY1, color, trans, priority, caller);
        canvas.DrawLine(x2, y2, x1 + arrowEndX2, y1 + arrowEndY2, color, trans, priority, caller);
    }
}