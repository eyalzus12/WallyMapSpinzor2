using System.Linq;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class ComboPart : IDeserializable, ISerializable
{
    public abstract int TypeIndex { get; }
    public abstract string TypeName { get; }
    public abstract bool TwoPartData { get; }

    public static ComboPart? New(string type) => type switch
    {
        "PowerCast" => new PowerCast(),
        "PowerHit" => new PowerHit(),
        "PowerEnd" => new PowerEnd(),
        "ActiveInput" => new ActiveInput(),
        "Throw" => new Throw(),
        "Jump" => new Jump(),
        "Dodge" => new Dodge(),
        "Dash" => new Dash(),
        "Pickup" => new Pickup(),
        "HitByEnemy" => new HitByEnemy(),
        _ => null,
    };

    // CmdOvr, Data, and Flags all mean different things for different ComboParts
    // see each type for info

    public uint CmdOvr { get; set; }
    public uint Data { get; set; } // if TwoPartData is true, upper 16 bits is second part
    public ComboPartEvalFlags Eval { get; set; }
    public uint Flags { get; set; }
    public string? Hint { get; set; }
    public uint Time { get; set; }

    public void Deserialize(XElement e)
    {
        CmdOvr = e.GetUIntAttribute("CmdOvr", 0);

        if (TwoPartData)
        {
            string[] data = e.GetAttribute("Data").Split('|');
            Data = uint.Parse(data[0]);
            if (data.Length > 1) Data |= uint.Parse(data[1]) << 16;
        }
        else
        {
            Data = e.GetUIntAttribute("Data", 0);
        }

        Eval = e.GetAttributeOrNull("Eval")?.ToUpperInvariant().Split('|').Select((eval) => eval switch
        {
            "HIDE" => ComboPartEvalFlags.HIDE,
            "IGNORE" => ComboPartEvalFlags.IGNORE,
            "NODATA" => ComboPartEvalFlags.NODATA,
            "NOFLAG" => ComboPartEvalFlags.NOFLAG,
            "SUB" => ComboPartEvalFlags.SUB,
            _ => (ComboPartEvalFlags)0,
        }).Aggregate((a, v) => a | v) ?? 0;

        Flags = e.GetUIntAttribute("Flags", 0);

        Hint = e.GetAttributeOrNull("Hint");

        Time = e.GetUIntAttribute("Time", 0);
    }

    public void Serialize(XElement e)
    {

    }

    /*
    attack.
    Data = power id. if two parts, both are options.
    Flags = 1 if gc, 0 otherwise
    CmdOvr = N/A
    */
    public class PowerCast : ComboPart
    {
        public override int TypeIndex => 1;
        public override string TypeName => "PowerCast";
        public override bool TwoPartData => true;
    }

    /*
    attack hit
    Data = power id. if two parts, both are options.
    Flags = a lot... notables:
        2 - gc
        131072 - dashjump
        262144 - active input version
        2097152 - exhausted version
        4194304 - charge version
        67108864 - cd cancel
    CmdOvr - N/A
    */
    public class PowerHit : ComboPart
    {
        public override int TypeIndex => 2;
        public override string TypeName => "PowerHit";
        public override bool TwoPartData => true;
    }

    /*
    attack end.
    Data = power id. if two parts, both are options.
    Flags = N/A
    CmdOvr = N/A
    */
    public class PowerEnd : ComboPart
    {
        public override int TypeIndex => 3;
        public override string TypeName => "PowerEnd";
        public override bool TwoPartData => true;
    }

    /*
    active input.
    Data = ?
    Flags = active input version
        1 - up
        2 - down
        4 - back
        8 - forward
    CmdOvr = ?
    */
    public class ActiveInput : ComboPart
    {
        public override int TypeIndex => 4;
        public override string TypeName => "ActiveInput";
        public override bool TwoPartData => true;
    }

    /*
    throw.
    Data = N/A
    Flags = N/A
    CmdOvr = N/A
    */
    public class Throw : ComboPart
    {
        public override int TypeIndex => 5;
        public override string TypeName => "Throw";
        public override bool TwoPartData => false;
    }

    /*
    jump.
    Data = N/A
    Flags = 1 if dashjump, 0 otherwise
    CmdOvr = N/A
    */
    public class Jump : ComboPart
    {
        public override int TypeIndex => 6;
        public override string TypeName => "Jump";
        public override bool TwoPartData => false;
    }

    /*
    dodge.
    Data = direction:
        0 - neutral
        1 - side
        2 - up
        4 - down
    Flags = type:
        1 - cd
        2 - chain dodge
        4 - aerial and not cd
    CmdOvr = N/A
    */
    public class Dodge : ComboPart
    {
        public override int TypeIndex => 7;
        public override string TypeName => "Dodge";
        public override bool TwoPartData => false;
    }

    /*
    dash.
    Data = type:
        1 - standard forward dash
        2 - N/A
        3 - backdash
        4 - dash out of dash
        5 - dash out of backdash
        6 - platform cancel / floor cancel dash
        7 - backdash out of dash.
    Flags = N/A
    CmdOvr = N/A
    */
    public class Dash : ComboPart
    {
        public override int TypeIndex => 8;
        public override string TypeName => "Dash";
        public override bool TwoPartData => false;
    }

    /*
    item pickup.
    Data = N/A
    Flags = N/A
    CmdOvr = N/A
    */
    public class Pickup : ComboPart
    {
        public override int TypeIndex => 9;
        public override string TypeName => "Pickup";
        public override bool TwoPartData => false;
    }

    /*
    getting hit.
    Data = ?
    Flags = ?
    CmdOvr = ?
    */
    public class HitByEnemy : ComboPart
    {
        public override int TypeIndex => 10;
        public override string TypeName => "HitByEnemy";
        public override bool TwoPartData => true;
    }
}