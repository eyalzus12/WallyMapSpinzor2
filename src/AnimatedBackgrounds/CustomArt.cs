using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class CustomArt : IDeserializable
{
    public bool Right{get; set;}
    public int Type{get; set;}
    public string FileName{get; set;} = null!;
    public string Name{get; set;} = null!;

    //idfk what's going on here

    public void Deserialize(XElement element)
    {
        Right = false;
        Type = 0;

        string str = element.Value;
        int sep = str.IndexOf(':');
        if(sep != -1)
        {
            var prefix = str[0..sep].ToUpper();
            str = str[(sep+1)..];
            if(prefix == "RIGHT")
            {
                Right = true;
            }
            else if(prefix == "C")
            {
                Type = 2;
            }
            else if(prefix == "W")
            {
                Type = 1;
            }
        }

        string[] parts = str.Split('/');
        FileName = parts[0];
        Name = parts[1];
    }
}