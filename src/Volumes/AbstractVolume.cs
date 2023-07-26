using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractVolume : IDeserializable
{
    //yes, brawlhalla defines those as ints.
    public int X{get; set;}
    public int Y{get; set;}
    public int W{get; set;}
    public int H{get; set;}
    public int Team{get; set;}
    public int ID{get; set;}
    
    public virtual void Deserialize(XElement element)
    {
        X = element.GetIntAttribute("X");
        Y = element.GetIntAttribute("Y");
        W = element.GetIntAttribute("W");
        H = element.GetIntAttribute("H");
        Team = element.GetIntAttribute("Team");
        ID = element.GetIntAttribute("ID", 0);
    }
}