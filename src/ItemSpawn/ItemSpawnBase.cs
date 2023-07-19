using System.Xml.Serialization;

namespace WallyMapSpinzor2;

public class ItemSpawnBase : HasRectBase
{

    [XmlType(IncludeInSchema=false)]
    public enum ItemSpawnType
    {
        None,
        ItemSpawn,
        ItemInitSpawn,
        ItemSet,
        TeamItemInitSpawn
    }

    public virtual ItemSpawnType Type => ItemSpawnType.None;
}