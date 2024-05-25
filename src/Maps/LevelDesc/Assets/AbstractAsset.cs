using System;
using System.IO;
using System.Xml.Linq;

namespace WallyMapSpinzor2;

public abstract class AbstractAsset : ISerializable, IDeserializable, IDrawable
{
    public string? AssetName { get; set; }
    public double Rotation { get; set; }
    public double ScaleX { get; set; }
    public double ScaleY { get; set; }
    public double H { get; set; }
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public virtual void Deserialize(XElement e)
    {
        AssetName = e.GetAttributeOrNull("AssetName");
        Rotation = e.GetFloatAttribute("Rotation", 0);
        double scale = e.GetFloatAttribute("Scale", 1);
        ScaleX = e.GetFloatAttribute("ScaleX", scale);
        ScaleY = e.GetFloatAttribute("ScaleY", scale);
        H = e.GetFloatAttribute("H", 0);
        W = e.GetFloatAttribute("W", 0);
        X = e.GetFloatAttribute("X", 0);
        Y = e.GetFloatAttribute("Y", 0);
    }

    public virtual void Serialize(XElement e)
    {
        if (AssetName is not null)
            e.SetAttributeValue("AssetName", AssetName);

        if (Rotation != 0)
            e.SetAttributeValue("Rotation", Rotation);

        if (ScaleX == ScaleY)
        {
            if (ScaleX != 1)
                e.SetAttributeValue("Scale", ScaleX);
        }
        else
        {
            if (ScaleX != 1)
                e.SetAttributeValue("ScaleX", ScaleX);
            if (ScaleY != 1)
                e.SetAttributeValue("ScaleY", ScaleY);
        }

        if (H != 0)
            e.SetAttributeValue("H", H);
        if (W != 0)
            e.SetAttributeValue("W", W);
        if (X != 0)
            e.SetAttributeValue("X", X);
        if (Y != 0)
            e.SetAttributeValue("Y", Y);
    }

    public virtual void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state)
    {
        if (!config.ShowAssets)
            return;

        if (AssetName is null) return;

        if (context.AssetDir is null)
            throw new InvalidOperationException("Attempting to draw an asset, but the render context is missing the AssetDir.");

        string path = Path.Combine(context.AssetDir, AssetName);
        canvas.DrawTextureRect(path, 0, 0, W, H, trans * Transform, DrawPriorityEnum.MIDGROUND, this);
    }

    public Transform Transform =>
        Transform.CreateFrom(
            x: X,
            y: Y,
            rot: Rotation * Math.PI / 180,
            scaleX: ScaleX,
            scaleY: ScaleY
        );
}