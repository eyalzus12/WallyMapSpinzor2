using System.Globalization;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class ColorSwap : IDeserializable, ISerializable
{
    public uint OldColor { get; set; }
    public uint NewColor { get; set; }

    public void Deserialize(XElement e)
    {
        string str = e.Value;
        string[] parts = str.Split('=');
        if (parts.Length != 2)
        {
            //TODO: log error
        }

        string oldColor = parts[0];
        if (oldColor[0] != '0')
        {
            //this lookups into a map of GfxType, but that map is never given values?
        }
        else
        {
            OldColor = uint.Parse(oldColor, CultureInfo.InvariantCulture);
        }

        string newColor = parts[1];
        if (newColor[0] != '0')
        {
            //this lookups into a map of GfxType, but that map is never given values?
        }
        else
        {
            NewColor = uint.Parse(newColor, CultureInfo.InvariantCulture);
        }
    }

    public void Serialize(XElement e)
    {
        e.SetValue($"0{OldColor}=0{NewColor}");
    }
}