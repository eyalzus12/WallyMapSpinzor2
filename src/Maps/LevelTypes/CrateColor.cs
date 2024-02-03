namespace WallyMapSpinzor2;

public readonly record struct CrateColor(byte R, byte G, byte B)
{
    public static CrateColor FromHex(uint hex) => new
    (
        R: (byte)(hex >> 16),
        G: (byte)(hex >> 08),
        B: (byte)(hex >> 00)
    );

    public string ToHexString() => $"0x{R:X2}{G:X2}{B:X2}";
}
