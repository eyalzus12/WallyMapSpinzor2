using System.Xml.Linq;

namespace WallyMapSpinzor2;

public class WaveData : IDeserializable, ISerializable, IDrawable
{
    public uint ID { get; set; }
    public double? Speed { get; set; }
    public double? Speed3 { get; set; }
    public double? Speed4 { get; set; }
    public uint LoopIdx { get; set; }
    public CustomPath[] CustomPaths { get; set; } = null!;
    public Group[] Groups { get; set; } = null!;

    public void Deserialize(XElement e)
    {
        ID = e.GetUIntAttribute("ID", 0);
        Speed = e.GetDoubleAttributeOrNull("Speed");
        Speed3 = e.GetDoubleAttributeOrNull("Speed3");
        Speed4 = e.GetDoubleAttributeOrNull("Speed4");
        LoopIdx = e.GetUIntAttribute("LoopIdx", 0);
        CustomPaths = e.DeserializeChildrenOfType<CustomPath>();
        Groups = e.DeserializeChildrenOfType<Group>();

        foreach (CustomPath cp in CustomPaths) cp.Parent = this;
        foreach (Group g in Groups) g.Parent = this;
    }

    public void Serialize(XElement e)
    {
        e.SetAttributeValue("ID", ID);

        if (Speed is not null)
            e.SetAttributeValue("Speed", Speed);
        if (Speed3 is not null)
            e.SetAttributeValue("Speed3", Speed3);
        if (Speed4 is not null)
            e.SetAttributeValue("Speed4", Speed4);

        if (LoopIdx != 0)
            e.SetAttributeValue("LoopIdx", LoopIdx);

        e.AddManySerialized(CustomPaths);
        e.AddManySerialized(Groups);
    }

    public void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (ID != config.HordeWave)
            return;
        if (config.HordePathType == RenderConfig.PathConfigEnum.CUSTOM && CustomPaths.Length != 0)
        {
            int pathIndex = BrawlhallaMath.SafeMod(config.HordePathIndex, CustomPaths.Length);
            CustomPaths[pathIndex].DrawOn(canvas, trans, config, context, state);
        }
    }

    public double GetSpeed(int players) => players switch
    {
        >= 4 when Speed4 is not null => Speed4.Value,
        >= 3 when Speed3 is not null => Speed3.Value,
        _ when Speed is not null => Speed.Value,
        _ => 8
    };
}