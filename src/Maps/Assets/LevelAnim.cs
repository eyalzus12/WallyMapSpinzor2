using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnim : IDeserializable, ISerializable
{
    public string InstanceName{get; set;} = null!;
    public string AssetName{get; set;} = null!;
    public double X{get; set;}
    public double Y{get; set;}
    
    public bool Foreground => InstanceName.StartsWith("am_Foreground");
    public bool Background => InstanceName.StartsWith("am_Background");

    public void Deserialize(XElement element)
    {
        InstanceName = element.GetAttribute("InstanceName");
        AssetName = element.GetAttribute("AssetName");
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
    }

    public XElement Serialize()
    {
        XElement e = new("LevelAnim");

        e.SetAttributeValue("InstanceName", InstanceName);
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());

        return e;
    }
}