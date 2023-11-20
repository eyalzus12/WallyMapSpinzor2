namespace WallyMapSpinzor2;

public interface ICanvas<TTexture> where TTexture : ITexture
{
    //draw a circle
    void DrawCircle(double X, double Y, double R, Color c, Transform t, DrawPriorityEnum p);
    //draw a line
    void DrawLine(double X1, double Y1, double X2, double Y2, Color c, Transform t, DrawPriorityEnum p);
    //draw a line that has 2 colors. used to indicate teams.
    void DrawLineMultiColor(double X1, double Y1, double X2, double Y2, Color[] cs, Transform t, DrawPriorityEnum p);
    //draw rect
    void DrawRect(double X, double Y, double W, double H, bool filled, Color c, Transform t, DrawPriorityEnum p);
    //draw text. possibly multiline.
    void DrawString(double X, double Y, string text, double fontSize, Color c, Transform t, DrawPriorityEnum p);
    //load a texture from a file from a path
    TTexture LoadTextureFromPath(string path);
    //load a texture from an swf file
    TTexture LoadTextureFromSWF(string filePath, string name);
    //TTexture LoadTextureFromANM(string filePath, string name);

    //draw a texture
    public void DrawTexture(double X, double Y, TTexture texture, Transform t, DrawPriorityEnum p);
    //draw a texture, resizing it to fit inside a rectangle
    public void DrawTextureRect(double X, double Y, double W, double H, TTexture texture, Transform t, DrawPriorityEnum p);

    public void ClearTextureCache();
}