using System;

namespace WallyMapSpinzor2;

[Flags]
public enum ComboInputFlags : ushort
{
    None = 0,
    AimUp = 0b0000_0000_00_0001,
    Drop = 0b0000_0000_00_0010,
    MoveLeft = 0b0000_0000_00_0100,
    MoveRight = 0b0000_0000_00_1000,
    Jump = 0b0000_0000_01_0000,
    PrioritiseNeutralOverSide = 0b0000_0000_10_0000,
    HeavyAttack = 0b0000_0001_00_0000,
    LightAttack = 0b0000_0010_00_0000,
    DodgeDash = 0b0000_0100_00_0000,
    PickUpThrow = 0b0000_1000_00_0000,
    TauntUp = 0b0001_0000_00_0000,
    TauntRight = 0b0010_0000_00_0000,
    TauntDown = 0b0100_0000_00_0000,
    TauntLeft = 0b1000_0000_00_0000,
}