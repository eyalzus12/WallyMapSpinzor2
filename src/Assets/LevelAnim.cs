using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class LevelAnim : IDeserializable
{
    public string InstanceName{get; set;} = "";
    public string AssetName{get; set;} = "";
    public double X{get; set;}
    public double Y{get; set;}
    
    public bool Foreground{get; set;}
    public bool Background{get; set;}

    public void Deserialize(XElement element)
    {
        InstanceName = element.GetAttribute("InstanceName");
        AssetName = element.GetAttribute("AssetName");
        X = element.GetFloatAttribute("X");
        Y = element.GetFloatAttribute("Y");
        
        Foreground = InstanceName.StartsWith("am_Foreground");
        Background = InstanceName.StartsWith("am_Background");
    }
}