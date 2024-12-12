using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LessonEntityType : IDeserializable, ISerializable
{
    public EntityRoleEnum Role { get; set; }
    public string HeroType { get; set; } = null!; // player and sensei should have the same
    public string CostumeType { get; set; } = null!;
    public uint WeaponIndex { get; set; }
    public double Position_X { get; set; }
    public double Position_Y { get; set; }
    public bool FacingLeft { get; set; }
    public double StartingDamage { get; set; }
    public string? BotReaction { get; set; }

    public void Deserialize(XElement e)
    {
        Role = e.GetEnumAttribute<EntityRoleEnum>("Role");
        HeroType = e.GetElement("HeroType");
        CostumeType = e.GetElement("CostumeType");
        WeaponIndex = e.GetUIntElement("WeaponIndex");
        string[] Position = e.GetElement("Position").Split(',');
        if (Position.Length != 2) throw new SerializationException($"LessonTypeEntity element {e} has invalid Position");
        Position_X = double.Parse(Position[0]);
        Position_Y = double.Parse(Position[1]);
        FacingLeft = e.GetBoolElement("FacingLeft", false);
        StartingDamage = e.GetDoubleElement("StartingDamage", 0);
        BotReaction = e.GetElementOrNull("BotReaction");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("Role", Role);
        e.AddChild("HeroType", HeroType);
        e.AddChild("CostumeType", CostumeType);
        e.AddChild("WeaponIndex", WeaponIndex);
        e.AddChild("Position", $"{Position_X},{Position_Y}");
        if (FacingLeft) e.AddChild("FacingLeft", "true");
        if (StartingDamage != 0) e.AddChild("StartingDamage", StartingDamage);
        if (BotReaction is not null) e.AddChild("BotReaction", BotReaction);
    }
}