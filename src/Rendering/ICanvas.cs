namespace WallyMapSpinzor2;

public interface ICanvas
{
    //draw a circle
    void DrawCircle(double x, double y, double radius, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw a line
    void DrawLine(double x1, double y1, double x2, double y2, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw a line that has multiple colors. used to indicate teams.
    void DrawLineMultiColor(double x1, double y1, double x2, double y2, Color[] colors, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw rect
    void DrawRect(double x, double y, double w, double h, bool filled, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw text. possibly multiline.
    void DrawString(double x, double y, string text, double fontSize, Color color, Transform trans, DrawPriorityEnum priority, object? caller);

    //draw a texture
    public void DrawTexture(string path, double x, double y, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw an animation
    public void DrawAnim(Gfx gfx, string animName, long frame, Transform trans, DrawPriorityEnum priority, object? caller, uint? loopLimit = null);
    //get the number of frames in an animation. returns null if animation hasn't loaded yet.
    public uint? GetAnimationFrameCount(Gfx gfx, string animName);
    //draw a texture, resizing it to fit inside a rectangle
    public void DrawTextureRect(string path, double x, double y, double? w, double? h, Transform trans, DrawPriorityEnum priority, object? caller);

    public void ClearTextureCache();
}