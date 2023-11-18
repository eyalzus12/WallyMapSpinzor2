using System.Globalization;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class XmlExtensions
{
    public static bool HasAttribute(this XElement element, string attribute) => element.Attributes(attribute).Any();
    public static string GetAttribute(this XElement element, string attribute, string @default = "") => element.Attributes(attribute).FirstOrDefault()?.Value ?? @default;
    public static bool GetBoolAttribute(this XElement element, string attribute, bool @default = false) => GetAttribute(element, attribute, @default.ToString()).ToUpperInvariant() == "TRUE";
    public static int GetIntAttribute(this XElement element, string attribute, int @default = 0) => int.Parse(GetAttribute(element, attribute, @default.ToString()), CultureInfo.InvariantCulture);
    public static double GetFloatAttribute(this XElement element, string attribute, double @default = 0.0f) => double.Parse(GetAttribute(element, attribute, @default.ToString()), CultureInfo.InvariantCulture);
    public static string? GetNullableAttribute(this XElement element, string attribute) => element.Attributes(attribute).FirstOrDefault()?.Value;
    public static bool? GetNullableBoolAttribute(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetNullableAttribute(element, attribute));
    public static int? GetNullableIntAttribute(this XElement element, string attribute) => Utils.ParseIntOrNull(GetNullableAttribute(element, attribute));
    public static double? GetNullableFloatAttribute(this XElement element, string attribute) => Utils.ParseFloatOrNull(GetNullableAttribute(element, attribute));
}