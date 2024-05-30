namespace WallyMapSpinzor2;

public readonly record struct Color(byte R, byte G, byte B, byte A)
{
    public static Color FromHex(uint i) => new
    (
        R: (byte)(i >> 24),
        G: (byte)(i >> 16),
        B: (byte)(i >> 08),
        A: (byte)(i >> 00)
    );

    public uint ToHex() =>
        (uint)(R << 24) |
        (uint)(G << 16) |
        (uint)(B << 08) |
        (uint)(A << 00);
}