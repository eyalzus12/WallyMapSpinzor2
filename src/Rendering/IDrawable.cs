namespace WallyMapSpinzor2;

public interface IDrawable
{
    void DrawOn(ICanvas canvas, Transform trans, RenderConfig config, RenderContext context, RenderState state);
}