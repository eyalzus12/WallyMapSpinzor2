namespace WallyMapSpinzor2;

public interface ICanvas<TTexture> where TTexture : ITexture
{
    void DrawCircle(double X, double Y, double R, Color c, Transform t, DrawPriorityEnum p);
    void DrawLine(double X1, double Y1, double X2, double Y2, Color c, Transform t, DrawPriorityEnum p);
    void DrawRect(double X, double Y, double W, double H, bool filled, Color c, Transform t, DrawPriorityEnum p);
    void DrawString(double X, double Y, string text, double fontSize, Color c, Transform t, DrawPriorityEnum p);
    void DrawStringOutline(double X, double Y, string text, double fontSize, Color c, Transform t, DrawPriorityEnum p);
    TTexture LoadTextureFromPath(string path);
    TTexture LoadTextureFromSWF(string filePath, string name);
    //TTexture LoadTextureFromANM(string filePath, string name);
    public void DrawTexture(double X, double Y, TTexture texture, Transform t, DrawPriorityEnum p);
    public void DrawTextureRect(double X, double Y, double W, double H, TTexture texture, Transform t, DrawPriorityEnum p);
}