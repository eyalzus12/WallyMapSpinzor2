using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractPressurePlateCollision : AbstractCollision
{
    public double AnimOffsetX{get; set;}
    public double AnimOffsetY{get; set;}
    public double AnimRotation{get; set;}
    public string AssetName{get; set;} = null!;
    public int Cooldown{get; set;}
    public bool FaceLeft{get; set;}
    public List<double> FireOffsetX{get; set;} = null!;
    public List<double> FireOffsetY{get; set;} = null!;
    public string? PlatID{get; set;}
    public List<string> TrapPowers{get; set;} = null!;

    public override void Deserialize(XElement e)
    {
        base.Deserialize(e);
        AnimOffsetX = e.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = e.GetFloatAttribute("AnimOffsetY", 0);
        AnimRotation = e.GetFloatAttribute("AnimRotation");
        AssetName = e.GetAttribute("AssetName");
        Cooldown = e.GetIntAttribute("Cooldown", 3000);
        FaceLeft = e.GetBoolAttribute("FaceLeft", false);
        FireOffsetX = e.GetAttribute("FireOffsetX").Split(',').Select(double.Parse).ToList();
        FireOffsetY = e.GetAttribute("FireOffsetY").Split(',').Select(double.Parse).ToList();
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10}; //wtf bmg
        PlatID = e.GetNullableAttribute("PlatID");
        TrapPowers = e.GetAttribute("TrapPowers").Split(',').ToList();
    }

    public override void Serialize(XElement e)
    {
        e.SetAttributeValue("AnimOffsetX", AnimOffsetX.ToString());
        e.SetAttributeValue("AnimOffsetY", AnimOffsetY.ToString());
        if(AnimRotation != 0)
            e.SetAttributeValue("AnimRotation", AnimRotation.ToString());
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("Cooldown", Cooldown.ToString());
        e.SetAttributeValue("FaceLeft", FaceLeft.ToString().ToLower());
        e.SetAttributeValue("FireOffsetX", string.Join(',', FireOffsetX));
        e.SetAttributeValue("FireOffsetY", string.Join(',', FireOffsetY));
        if(PlatID is not null)
            e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("TrapPowers", string.Join(',', TrapPowers));
        base.Serialize(e);
    }
}