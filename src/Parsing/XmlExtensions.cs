using System;
using System.Globalization;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class XmlExtensions
{
    public static bool HasAttribute(this XElement element, string attribute) => element.Attribute(attribute) is not null;

    // get attribute value. if invalid format - error. if doesn't exist - given default (and error if no default is given).
    public static string GetAttribute(this XElement element, string attribute, string? @default = null) => element.Attribute(attribute)?.Value ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");
    public static bool GetBoolAttribute(this XElement element, string attribute, bool? @default = null) => element.GetAttribute(attribute, @default?.ToString()).Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
    public static int GetIntAttribute(this XElement element, string attribute, int? @default = null) => int.Parse(element.GetAttribute(attribute, @default?.ToString()), CultureInfo.InvariantCulture);
    public static double GetDoubleAttribute(this XElement element, string attribute, double? @default = null) => double.Parse(element.GetAttribute(attribute, @default?.ToString()), CultureInfo.InvariantCulture);

    // get attribute value. if invalid format - error. if doesn't exist - null.
    public static string? GetAttributeOrNull(this XElement element, string attribute) => element.Attribute(attribute)?.Value;
    public static bool? GetBoolAttributeOrNull(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetAttributeOrNull(element, attribute));
    public static int? GetIntAttributeOrNull(this XElement element, string attribute) => Utils.ParseIntOrNull(GetAttributeOrNull(element, attribute));
    public static double? GetDoubleAttributeOrNull(this XElement element, string attribute) => Utils.ParseDoubleOrNull(GetAttributeOrNull(element, attribute));

    // get element value. if invalid format - error. if doesn't exist - null.
    public static bool? GetBoolElementOrNull(this XElement element, string name) => Utils.ParseBoolOrNull(element.GetElementValue(name));
    public static int? GetIntElementOrNull(this XElement element, string name) => Utils.ParseIntOrNull(element.GetElementValue(name));
    public static uint? GetUIntElementOrNull(this XElement element, string name) => Utils.ParseUIntOrNull(element.GetElementValue(name));
    public static double? GetDoubleElementOrNull(this XElement element, string name) => Utils.ParseDoubleOrNull(element.GetElementValue(name));
    public static E? GetEnumElementOrNull<E>(this XElement element, string name) where E : struct, Enum => Utils.ParseEnumOrNull<E>(element.GetElementValue(name));

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