using System;

namespace WallyMapSpinzor2;

public interface IDrawable
{
    void DrawOn(ICanvas canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data);
}