using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class XmlExtensions
{
    public static bool HasAttribute(this XElement element, string attribute) => element.Attributes(attribute).Any();
    public static string GetAttribute(this XElement element, string attribute, string @default = "") => element.Attributes(attribute).FirstOrDefault()?.Value ?? @default;
    public static bool GetBoolAttribute(this XElement element, string attribute, bool @default = false) => GetAttribute(element, attribute, @default.ToString()).ToUpper() == "TRUE";
    public static int GetIntAttribute(this XElement element, string attribute, int @default = 0) => int.Parse(GetAttribute(element, attribute, @default.ToString()));
    public static double GetFloatAttribute(this XElement element, string attribute, double @default = 0.0f) => double.Parse(GetAttribute(element, attribute, @default.ToString()));
    public static string? GetNullableAttribute(this XElement element, string attribute) => element.Attributes(attribute).FirstOrDefault()?.Value;
    public static bool? GetNullableBoolAttribute(this XElement element, string attribute) => Utils.ParseBoolOrNull(GetNullableAttribute(element, attribute));
    public static int? GetNullableIntAttribute(this XElement element, string attribute) => Utils.ParseIntOrNull(GetNullableAttribute(element, attribute));
    public static double? GetNullableFloatAttribute(this XElement element, string attribute) => Utils.ParseFloatOrNull(GetNullableAttribute(element, attribute));
    public static T DeserializeTo<T>(this XElement element) where T : IDeserializable, new()
    {
        T t = new(); t.Deserialize(element); return t;
    }
    public static List<T> DeserializeChildrenOfType<T>(this XElement element) where T : IDeserializable, new()
        => element.Elements(typeof(T).Name).Select(DeserializeTo<T>).ToList();
    public static T? DeserializeChildOfType<T>(this XElement element) where T : IDeserializable, new()
        => element.Elements(typeof(T).Name).Select(DeserializeTo<T>).FirstOrDefault();
    public static List<AbstractCollision> DeserializeCollisionChildren(this XElement element) =>
        element.Elements().Select(DeserializeCollision).Where(c => c is not null)
        .ToList()!;
    public static AbstractCollision? DeserializeCollision(this XElement element) => element.Name.LocalName switch
    {
        nameof(HardCollision) => element.DeserializeTo<HardCollision>(),
        nameof(SoftCollision) => element.DeserializeTo<SoftCollision>(),
        nameof(NoSlideCollision) => element.DeserializeTo<NoSlideCollision>(),
        nameof(BouncyHardCollision) => element.DeserializeTo<BouncyHardCollision>(),
        nameof(BouncySoftCollision) => element.DeserializeTo<BouncySoftCollision>(),
        nameof(BouncyNoSlideCollision) => element.DeserializeTo<BouncyNoSlideCollision>(),
        nameof(GameModeHardCollision) => element.DeserializeTo<GameModeHardCollision>(),
        nameof(StickyCollision) => element.DeserializeTo<StickyCollision>(),
        nameof(TriggerCollision) => element.DeserializeTo<TriggerCollision>(),
        nameof(ItemIgnoreCollision) => element.DeserializeTo<ItemIgnoreCollision>(),
        nameof(PressurePlateCollision) => element.DeserializeTo<PressurePlateCollision>(),
        nameof(SoftPressurePlateCollision) => element.DeserializeTo<SoftPressurePlateCollision>(),
        _ => null
    };

    public static List<AbstractItemSpawn> DeserializeItemSpawnChildren(this XElement element) =>
        element.Elements().Select(DeserializeItemSpawn).Where(c => c is not null)
        .ToList()!;

    public static AbstractItemSpawn? DeserializeItemSpawn(this XElement element) => element.Name.LocalName switch
    {
        nameof(ItemSpawn) => element.DeserializeTo<ItemSpawn>(),
        nameof(ItemInitSpawn) => element.DeserializeTo<ItemInitSpawn>(),
        nameof(ItemSet) => element.DeserializeTo<ItemSet>(),
        nameof(TeamItemInitSpawn) => element.DeserializeTo<TeamItemInitSpawn>(),
        _ => null
    };

    public static List<AbstractVolume> DeserializeVolumeChildren(this XElement element) =>
        element.Elements().Select(DeserializeVolume).Where(c => c is not null)
        .ToList()!;

    public static AbstractVolume? DeserializeVolume(this XElement element) => element.Name.LocalName switch
    {
        nameof(Volume) => element.DeserializeTo<Volume>(),
        nameof(Goal) => element.DeserializeTo<Goal>(),
        nameof(NoDodgeZone) => element.DeserializeTo<NoDodgeZone>(),
        _ => null
    };

    public static List<IKeyFrame> DeserializeKeyFrameChildren(this XElement element) =>
        element.Elements().Select(DeserializeKeyFrame).Where(c => c is not null)
        .ToList()!;

    public static IKeyFrame? DeserializeKeyFrame(this XElement element) => element.Name.LocalName switch
    {
        nameof(KeyFrame) => element.DeserializeTo<KeyFrame>(),
        nameof(Phase) => element.DeserializeTo<Phase>(),
        _ => null
    };
}