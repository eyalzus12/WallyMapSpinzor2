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
    public static string? GetAttributeOrNull(this XElement element, string attribute) => element.Attributes(attribute).FirstOrDefault()?.Value;
    public static bool? GetBoolAttributeOrNull(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetAttributeOrNull(element, attribute));
    public static int? GetIntAttributeOrNull(this XElement element, string attribute) => Utils.ParseIntOrNull(GetAttributeOrNull(element, attribute));
    public static double? GetFloatAttributeOrNull(this XElement element, string attribute) => Utils.ParseFloatOrNull(GetAttributeOrNull(element, attribute));

    public static void AddIfNotNull(this XElement element, string name, object? value)
    {
        if (value is not null) element.Add(new XElement(name, value));
    }

    public static string? GetElementValue(this XElement e, string child) => e.Element(child)?.Value;
}