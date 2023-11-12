namespace WallyMapSpinzor2;

public readonly record struct Color(byte R, byte G, byte B, byte A)
{
    public static Color FromHex(uint i) => new
    (
        R : (byte)(i >> 24),
        G : (byte)(i >> 16),
        B : (byte)(i >> 08),
        A : (byte)(i >> 00)
    );
}