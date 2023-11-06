using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractPressurePlateCollision : AbstractCollision
{
    public List<string> TrapPowers{get; set;} = null!;
    public List<double> FireOffsetX{get; set;} = null!;
    public List<double> FireOffsetY{get; set;} = null!;
    public string AssetName{get; set;} = null!;
    public int Cooldown{get; set;}
    public bool FaceLeft{get; set;}
    public double AnimOffsetX{get; set;}
    public double AnimOffsetY{get; set;}
    public string? PlatID{get; set;}
    public double AnimRotation{get; set;}

    public override void Deserialize(XElement element)
    {
        base.Deserialize(element);
        TrapPowers = element.GetAttribute("TrapPowers").Split(',').ToList();
        FireOffsetX = element.GetAttribute("FireOffsetX").Split(',').Select(double.Parse).ToList();
        FireOffsetY = element.GetAttribute("FireOffsetY").Split(',').Select(double.Parse).ToList();
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10}; //wtf bmg
        AssetName = element.GetAttribute("AssetName");
        Cooldown = element.GetIntAttribute("Cooldown", 3000);
        FaceLeft = element.GetBoolAttribute("FaceLeft", false);
        AnimOffsetX = element.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = element.GetFloatAttribute("AnimOffsetY", 0);
        PlatID = element.GetNullableAttribute("PlatID");
        AnimRotation = element.GetFloatAttribute("AnimRotation");
    }

    public override XElement Serialize()
    {
        XElement e = base.Serialize();

        e.SetAttributeValue("TrapPowers", string.Join(',', TrapPowers));
        e.SetAttributeValue("FireOffsetX", string.Join(',', FireOffsetX));
        e.SetAttributeValue("FireOffsetY", string.Join(',', FireOffsetY));
        e.SetAttributeValue("AssetName", AssetName);
        e.SetAttributeValue("Cooldown", Cooldown.ToString());
        e.SetAttributeValue("FaceLeft", FaceLeft.ToString().ToLower());
        if(AnimOffsetX != 0)
            e.SetAttributeValue("AnimOffsetX", AnimOffsetX.ToString());
        if(AnimOffsetY != 0)
            e.SetAttributeValue("AnimOffsetY", AnimOffsetY.ToString());
        if(PlatID is not null)
            e.SetAttributeValue("PlatID", PlatID);
        e.SetAttributeValue("AnimRotation", AnimRotation.ToString());

        return e;
    }
}