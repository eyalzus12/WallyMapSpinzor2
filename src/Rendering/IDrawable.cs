using System;

namespace WallyMapSpinzor2;

public interface IDrawable
{
    void DrawOn<T>(ICanvas<T> canvas, RenderConfig config, Transform trans, TimeSpan time, RenderData data)
        where T : ITexture;
}