using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class XmlExtensions
{
    public static bool HasAttribute(this XElement element, string attribute) => element.Attribute(attribute) is not null;

    // get attribute value. if invalid format - error. if doesn't exist - null.
    public static string? GetAttributeOrNull(this XElement element, string attribute) => element.Attribute(attribute)?.Value;
    public static bool? GetBoolAttributeOrNull(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetAttributeOrNull(element, attribute));
    public static int? GetIntAttributeOrNull(this XElement element, string attribute) => Utils.ParseIntOrNull(GetAttributeOrNull(element, attribute));
    public static uint? GetUIntAttributeOrNull(this XElement element, string attribute) => Utils.ParseUIntOrNull(GetAttributeOrNull(element, attribute));
    public static double? GetDoubleAttributeOrNull(this XElement element, string attribute) => Utils.ParseDoubleOrNull(GetAttributeOrNull(element, attribute));

    // get attribute value. if invalid format - error. if doesn't exist - given default (and error if no default is given).
    public static string GetAttribute(this XElement element, string attribute, string? @default = null) => GetAttributeOrNull(element, attribute) ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");
    public static bool GetBoolAttribute(this XElement element, string attribute, bool? @default = null) => GetBoolAttributeOrNull(element, attribute) ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");
    public static int GetIntAttribute(this XElement element, string attribute, int? @default = null) => GetIntAttributeOrNull(element, attribute) ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");
    public static uint GetUIntAttribute(this XElement element, string attribute, uint? @default = null) => GetUIntAttributeOrNull(element, attribute) ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");
    public static double GetDoubleAttribute(this XElement element, string attribute, double? @default = null) => GetDoubleAttributeOrNull(element, attribute) ?? @default ?? throw new SerializationException($"element {element} is missing required attribute {attribute}");

    // get element value. if invalid format - error. if doesn't exist - null.
    public static string? GetElementOrNull(this XElement element, string name) => element.Element(name)?.Value;
    public static bool? GetBoolElementOrNull(this XElement element, string name) => Utils.ParseBoolOrNull(element.GetElementOrNull(name));
    public static int? GetIntElementOrNull(this XElement element, string name) => Utils.ParseIntOrNull(element.GetElementOrNull(name));
    public static uint? GetUIntElementOrNull(this XElement element, string name) => Utils.ParseUIntOrNull(element.GetElementOrNull(name));
    public static double? GetDoubleElementOrNull(this XElement element, string name) => Utils.ParseDoubleOrNull(element.GetElementOrNull(name));
    public static E? GetEnumElementOrNull<E>(this XElement element, string name) where E : struct, Enum => Utils.ParseEnumOrNull<E>(element.GetElementOrNull(name));
    public static Color? GetColorElementOrNull(this XElement element, string name) => Utils.FromHexOrNull(element.GetUIntElementOrNull(name));

    // get element value. if invalid format - error. if doesn't exist - given default (and error if no default is given).
    public static string GetElement(this XElement element, string name, string? @default = null) => element.GetElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static bool GetBoolElement(this XElement element, string name, bool? @default = null) => element.GetBoolElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static int GetIntElement(this XElement element, string name, int? @default = null) => element.GetIntElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static uint GetUIntElement(this XElement element, string name, uint? @default = null) => element.GetUIntElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static double GetDoubleElement(this XElement element, string name, double? @default = null) => element.GetDoubleElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static E GetEnumElement<E>(this XElement element, string name, E? @default = null) where E : struct, Enum => element.GetEnumElementOrNull<E>(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");
    public static Color GetColorElement(this XElement element, string name, Color? @default = null) => element.GetColorElementOrNull(name) ?? @default ?? throw new SerializationException($"element {element} is missing required element {name}");

    public static void AddChild(this XElement element, string name, object? value)
    {
        element.Add(new XElement(name, value));
    }

    public static void AddIfNotNull(this XElement element, string name, object? value)
    {
        if (value is not null) element.Add(new XElement(name, value));
    }

    public static T DeserializeTo<T>(this XElement e) where T : IDeserializable, new()
    {
        T t = new(); t.Deserialize(e); return t;
    }

    public static T[] DeserializeChildrenOfType<T>(this XElement e) where T : IDeserializable, new()
        => [.. e.Elements(typeof(T).Name).Select(DeserializeTo<T>)];

    public static T? DeserializeChildOfType<T>(this XElement e) where T : class, IDeserializable, new()
        => e.Element(typeof(T).Name)?.DeserializeTo<T>();

    public static T DeserializeRequiredChildOfType<T>(this XElement e) where T : IDeserializable, new()
        => (e.Element(typeof(T).Name) ?? throw new SerializationException($"Element {e} is missing required child {typeof(T).Name}")).DeserializeTo<T>();

    public static XElement SerializeToXElement<T>(this T t) where T : ISerializable
    {
        XElement e = new(t.GetType().Name); t.Serialize(e); return e;
    }

    public static void AddSerialized<T>(this XElement e, T t) where T : ISerializable
        => e.Add(t.SerializeToXElement());

    public static void AddManySerialized<T>(this XElement e, IEnumerable<T> et) where T : ISerializable
    {
        foreach (T t in et)
            e.AddSerialized(t);
    }

    public static void AddSerializedIfNotNull<T>(this XElement e, T? t) where T : ISerializable
    {
        if (t is not null) e.AddSerialized(t);
    }
}