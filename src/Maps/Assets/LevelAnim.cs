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

    public void Deserialize(XElement e)
    {
        InstanceName = e.GetAttribute("InstanceName");
        AssetName = e.GetAttribute("AssetName");
        X = e.GetFloatAttribute("X");
        Y = e.GetFloatAttribute("Y");
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("InstanceName", InstanceName);
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("X", X.ToString());
        e.SetAttributeValue("Y", Y.ToString());
    }
}