using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomArt : IDeserializable, ISerializable
{
    public bool Right{get; set;}
    public int Type{get; set;}
    public string FileName{get; set;} = null!;
    public string Name{get; set;} = null!;

    //idfk what's going on here

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
        string prefix = Right?"RIGHT:":Type switch
        {
            1 => "W:",
            2 => "C:",
            _ => ""
        };
        e.SetValue($"{prefix}{FileName}/{Name}");
    }
}
