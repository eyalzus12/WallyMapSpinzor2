using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomArt : IDeserializable, ISerializable
{
    public bool Right { get; set; } = false;
    /*
    types:
    0 - none
    1 - weapon
    2 - costume
    3 - pickup (can't be set in xml)
    4 - flag? (can't be set in xml)
    5 - bot? (can't be set in xml)
    */
    public int Type { get; set; } = 0;
    public string FileName { get; set; } = null!;
    public string Name { get; set; } = null!;

    public void Deserialize(XElement e)
    {
        string str = e.Value;

        Right = str.StartsWith("RIGHT:");
        Type = Right ? 0 : (str.StartsWith("C:") ? 2 : (str.StartsWith("W:") ? 1 : 0));

        str = str[(str.IndexOf(':') + 1)..];
        string[] parts = str.Split('/');
        FileName = parts[0];
        Name = parts[1];
    }

    public void Serialize(XElement e)
    {
        string prefix = Right ? "RIGHT:" : Type switch
        {
            1 => "W:",
            2 => "C:",
            _ => ""
        };
        e.SetValue($"{prefix}{FileName}/{Name}");
    }
}