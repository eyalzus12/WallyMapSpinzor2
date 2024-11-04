using System;
using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelTypes : IDeserializable, ISerializable
{
    public LevelType[] Levels { get; set; } = null!;

    public const uint MAX_LEVEL_ID = 255;

    public void Deserialize(XElement e)
    {
        Levels = e.DeserializeChildrenOfType<LevelType>();
    }

    public void Serialize(XElement e)
    {
        e.AddManySerialized(Levels);
    }

    public void AddOrUpdateLevelType(LevelType lt)
    {
        int index = Array.FindIndex(Levels, l => l.LevelName == lt.LevelName);
        if (index == -1)
        {
            uint id = GetLargestLevelId() + 1;
            if (id > MAX_LEVEL_ID)
            {
                throw new ArgumentException($"Tried to add a leveltype with id bigger than {MAX_LEVEL_ID}");
            }

            lt.LevelID = id;
            Levels = [.. Levels, lt];
            return;
        }

        lt.LevelID = Levels[index].LevelID;
        Levels[index] = lt;
    }

    public uint GetLargestLevelId() => Levels.Select(l => l.LevelID).DefaultIfEmpty(0u).Max();
}