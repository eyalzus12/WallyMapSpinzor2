using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractPressurePlateCollision : AbstractCollision
{
    public List<string> TrapPowers{get; set;} = new();
    public List<double> FireOffsetX{get; set;} = new();
    public List<double> FireOffsetY{get; set;} = new();
    public string AssetName{get; set;} = "";
    public int Cooldown{get; set;}
    public bool FaceLeft{get; set;}
    public double AnimOffsetX{get; set;}
    public double AnimOffsetY{get; set;}
    public string PlatID{get; set;} = "";
    public double AnimRotation{get; set;}

    public override void Deserialize(XElement element)
    {
        base.Deserialize(element);
        TrapPowers = element.GetAttribute("TrapPowers").Split(',').ToList();
        FireOffsetX = element.GetAttribute("FireOffsetX").Split(',').Select(double.Parse).ToList();
        FireOffsetY = element.GetAttribute("FireOffsetY").Split(',').Select(double.Parse).ToList();
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10};
        AssetName = element.GetAttribute("AssetName");
        Cooldown = element.GetIntAttribute("Cooldown", 3000);
        FaceLeft = element.GetBoolAttribute("FaceLeft", false);
        AnimOffsetX = element.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = element.GetFloatAttribute("AnimOffsetY", 0);
        PlatID = element.GetAttribute("PlatID");
        AnimRotation = Utils.DegToRad(element.GetFloatAttribute("AnimRotation"));
    }
}