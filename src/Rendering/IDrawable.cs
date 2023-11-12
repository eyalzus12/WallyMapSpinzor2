namespace WallyMapSpinzor2;

public interface IDrawable
{
    void DrawOn<TTexture>
    (ICanvas<TTexture> canvas, GlobalRenderData rd, RenderSettings rs, Transform t, double time)
        where TTexture : ITexture;
}