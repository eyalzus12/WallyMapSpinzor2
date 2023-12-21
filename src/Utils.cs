using System.Globalization;

namespace WallyMapSpinzor2;

public static class Utils
{
    public static double? ParseFloatOrNull(string? s) => (s is null) ? null : double.Parse(s, CultureInfo.InvariantCulture);
    public static bool? ParseBoolOrNull(string? s) => (s is null) ? null : (s.ToUpperInvariant() == "TRUE");
    public static int? ParseIntOrNull(string? s) => (s is null) ? null : int.Parse(s, CultureInfo.InvariantCulture);
    public static uint? ParseUIntOrNull(string? s) => (s is null) ? null : (s.StartsWith("0x") ? Convert.ToUInt32(s, 16) : uint.Parse(s, CultureInfo.InvariantCulture));
}
