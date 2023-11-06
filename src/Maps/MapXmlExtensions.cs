using System.Xml.Linq;

namespace WallyMapSpinzor2;

public static class MapXmlExtensions
{
    public static T DeserializeTo<T>(this XElement element) where T : IDeserializable, new()
    {
        T t = new(); t.Deserialize(element); return t;
    }
    
    public static List<T> DeserializeChildrenOfType<T>(this XElement element) where T : IDeserializable, new()
        => element.Elements(typeof(T).Name).Select(DeserializeTo<T>).ToList();
    
    //thanks to lazy evaluation, this won't go over everything
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

    public static List<AbstractKeyFrame> DeserializeKeyFrameChildren(this XElement element) =>
        element.Elements().Select(DeserializeKeyFrame).Where(c => c is not null)
        .ToList()!;

    public static AbstractKeyFrame? DeserializeKeyFrame(this XElement element) => element.Name.LocalName switch
    {
        nameof(KeyFrame) => element.DeserializeTo<KeyFrame>(),
        nameof(Phase) => element.DeserializeTo<Phase>(),
        _ => null
    };
}