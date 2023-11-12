namespace WallyMapSpinzor2;

public interface IDrawable
{
    void DrawOn<TTexture>(ICanvas<TTexture> canvas, RenderSettings rs, Transform t) where TTexture : ITexture;
}