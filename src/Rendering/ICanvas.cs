namespace WallyMapSpinzor2;

public interface ICanvas<TTexture> where TTexture : ITexture
{
    //draw a circle
    void DrawCircle(double x, double y, double radius, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw a line
    void DrawLine(double x1, double y1, double x2, double y2, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw a line that has 2 colors. used to indicate teams.
    void DrawLineMultiColor(double x1, double y1, double x2, double y2, Color[] colors, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw rect
    void DrawRect(double x, double y, double w, double h, bool filled, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw text. possibly multiline.
    void DrawString(double x, double y, string text, double fontSize, Color color, Transform trans, DrawPriorityEnum priority, object? caller);
    //load a texture from a file from a path
    TTexture LoadTextureFromPath(string path);
    //load a texture from an swf file
    TTexture LoadTextureFromSWF(string filePath, string name);
    //TTexture LoadTextureFromANM(string filePath, string name);

    //draw a texture
    public void DrawTexture(double x, double y, TTexture texture, Transform trans, DrawPriorityEnum priority, object? caller);
    //draw a texture, resizing it to fit inside a rectangle
    public void DrawTextureRect(double x, double y, double w, double h, TTexture texture, Transform trans, DrawPriorityEnum priority, object? caller);

    public void ClearTextureCache();
}