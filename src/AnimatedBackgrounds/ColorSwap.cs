using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class ColorSwap : IDeserializable
{
    public int OldColor{get; set;}
    public int NewColor{get; set;}

    public void Deserialize(XElement element)
    {
        string str = element.Value;
        string[] parts = str.Split(',');
        if(parts.Length != 2)
        {
            //TODO: log
        }

        string oldColor = parts[0];
        if(oldColor[0] != '0')
        {
            //this lookups into a map of GfxType, but that map is never given values?
        }
        else
        {
            OldColor = int.Parse(oldColor);
        }

        string newColor = parts[1];
        if(newColor[0] != '0')
        {
            //this lookups into a map of GfxType, but that map is never given values?
        }
        else
        {
            NewColor = int.Parse(newColor);
        }
    }
}