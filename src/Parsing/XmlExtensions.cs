using System;
using System.Globalization;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class XmlExtensions
{
    public static bool HasAttribute(this XElement element, string attribute) => element.Attribute(attribute) is not null;
    public static string GetAttribute(this XElement element, string attribute, string @default = "") => element.Attribute(attribute)?.Value ?? @default;
    public static bool GetBoolAttribute(this XElement element, string attribute, bool @default = false) => element.GetAttribute(attribute, @default.ToString()).Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
    public static int GetIntAttribute(this XElement element, string attribute, int @default = 0) => int.Parse(element.GetAttribute(attribute, @default.ToString()), CultureInfo.InvariantCulture);
    public static double GetDoubleAttribute(this XElement element, string attribute, double @default = 0.0) => double.Parse(element.GetAttribute(attribute, @default.ToString()), CultureInfo.InvariantCulture);
    public static float GetFloatAttribute(this XElement element, string attribute, float @default = 0.0f) => float.Parse(element.GetAttribute(attribute, @default.ToString()), CultureInfo.InvariantCulture);

    public static string? GetAttributeOrNull(this XElement element, string attribute) => element.Attribute(attribute)?.Value;
    public static bool? GetBoolAttributeOrNull(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetAttributeOrNull(element, attribute));
    public static int? GetIntAttributeOrNull(this XElement element, string attribute) => Utils.ParseIntOrNull(GetAttributeOrNull(element, attribute));
    public static double? GetDoubleAttributeOrNull(this XElement element, string attribute) => Utils.ParseDoubleOrNull(GetAttributeOrNull(element, attribute));
    public static float? GetFloatAttributeOrNull(this XElement element, string attribute) => Utils.ParseFloatOrNull(GetAttributeOrNull(element, attribute));

    public static void AddChild(this XElement element, string name, object? value)
    {
        element.Add(new XElement(name, value));
    }

    public static void AddIfNotNull(this XElement element, string name, object? value)
    {
        if (value is not null) element.Add(new XElement(name, value));
    }

    public static string? GetElementValue(this XElement e, string child) => e.Element(child)?.Value;
}