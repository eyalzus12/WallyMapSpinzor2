using System;

namespace WallyMapSpinzor2;

[Flags]
public enum CollisionTypeFlags
{
    HARD = 1 << 0,
    SOFT = 1 << 1,
    TRIGGER = 1 << 2,
    STICKY = 1 << 3,
    NO_SLIDE = 1 << 4,
    ITEM_IGNORE = 1 << 5,
    BOUNCY = 1 << 6,
    GAMEMODE = 1 << 7,
    PRESSURE_PLATE = 1 << 8,
    LAVA = 1 << 9,
}