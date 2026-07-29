using System.Xml.Linq;

namespace WallyMapSpinzor2;

public sealed class ForegroundAsset : AbstractAsset, IDeserializable<ForegroundAsset>
{
    public ForegroundAsset() : base() { }
    private ForegroundAsset(XElement e) : base(e) { }
    public static ForegroundAsset Deserialize(XElement e) => new(e);

    public override DrawPriorityEnum DrawPriority => DrawPriorityEnum.FOREGROUND;
}