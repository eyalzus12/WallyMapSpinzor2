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

    public override void Deserialize(XElement element)
    {
        base.Deserialize(element);
        AnimOffsetX = element.GetFloatAttribute("AnimOffsetX", 0);
        AnimOffsetY = element.GetFloatAttribute("AnimOffsetY", 0);
        AnimRotation = element.GetFloatAttribute("AnimRotation");
        AssetName = element.GetAttribute("AssetName");
        Cooldown = element.GetIntAttribute("Cooldown", 3000);
        FaceLeft = element.GetBoolAttribute("FaceLeft", false);
        FireOffsetX = element.GetAttribute("FireOffsetX").Split(',').Select(double.Parse).ToList();
        FireOffsetY = element.GetAttribute("FireOffsetY").Split(',').Select(double.Parse).ToList();
        if(FireOffsetY.Count == 0) FireOffsetY = new(){-10}; //wtf bmg
        PlatID = element.GetNullableAttribute("PlatID");
        TrapPowers = element.GetAttribute("TrapPowers").Split(',').ToList();
    }

    public override XElement Serialize()
    {
        XElement e = new(GetType().Name);

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

        //hack to put attributes before the normal collision stuff
        XElement e2 = base.Serialize();
        foreach(XAttribute attr in e2.Attributes())
            e.SetAttributeValue(attr.Name, attr.Value);


        return e;
    }
}